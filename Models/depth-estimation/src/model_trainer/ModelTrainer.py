import os
import torch
import torch.nn as nn
import torch.optim as optim
from torch.utils.data import Dataset, DataLoader

class ModelTrainer:

    def __init__(self, model: nn.Module, dataset: Dataset, criterion=None, optimizer=None):
        self.model = model
        self.criterion = nn.MSELoss() if criterion is None else criterion
        self.optimizer = optim.Adam(self.model.parameters(), lr=0.001) if optimizer is None else optimizer 
        self.epoch_num: int = 1
        self.dataloader = DataLoader(dataset, batch_size=4, shuffle=True)
        self.make_checkpoints = False
        self.checkpoint_path = ""

    def need_checkpoints(self, checkpoint_path):
        self.make_checkpoints = True
        self.checkpoint_path = checkpoint_path
        if os.path.exists(checkpoint_path) == False:
            os.mkdir(checkpoint_path)
        return self

    def set_epochs(self, epoch_num: int):
        self.epoch_num = epoch_num
        return self

    def start_training(self):
        for epoch in range(self.epoch_num):
            self.train_model(epoch+1)

    def train_model(self, epoch: int):
        print(f"Start training, Epoch {epoch}")
        self.model.train()
        current_loss = self.proceed_pipeline()
        dataset_len = len(self.dataloader.dataset) #type: ignore
        epoch_loss = current_loss / dataset_len
        if (self.make_checkpoints):
            self.make_checkpoint(epoch, epoch_loss)
        print(f'Epoch {epoch}/{self.epoch_num}, Loss: {epoch_loss:.4f}')

    def proceed_pipeline(self):
        current_running_loss = 0.0
        for images, depths in self.dataloader:
            images, depths = self.load_data_to_gpu(images, depths)
            outputs = self.model(images)
            loss = self.calculate_loss(outputs, depths)
            current_running_loss += loss.item() * images.size(0)
        return current_running_loss
    
    def load_data_to_gpu(self, images, depths):
        return images.to(torch.device('cuda')), depths.to(torch.device('cuda'))

    def calculate_loss(self, outputs, depths):
        loss = self.criterion(outputs, depths)                
        self.optimizer.zero_grad()
        loss.backward()
        self.optimizer.step()
        return loss

    def make_checkpoint(self, epoch, loss):

        torch.save({
            'epoch': epoch,
            'model_state_dict': self.model.state_dict(),
            'optimizer_state_dict': self.optimizer.state_dict(),
            'loss': loss
        }, os.path.join(self.checkpoint_path, "checkpoint_{epoch}.json"))
        print("checkpoint maked")

    def save_model(self, save_path: str):
        model_scripted = torch.jit.script(self.model)
        model_scripted.save(os.path.join(save_path, 'model_scripted.pt'))
