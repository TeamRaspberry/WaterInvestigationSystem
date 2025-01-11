import os
import torch
from torch.nn import MSELoss, Module
import torch.optim as optim
from torch.utils.data import DataLoader

class ModelTrainer:

    def __init__(self, model: Module, dataset, criterion=None, optimizer=None):
        self.__model = model
        self.__criterion = MSELoss() if criterion is None else criterion
        self.__optimizer = optim.Adam(self.__model.parameters(), lr=0.001) if optimizer is None else optimizer 
        self.__epoch_num: int = 1
        self.__dataloader = DataLoader(dataset, batch_size=4, shuffle=True)
        self.__make_checkpoints = False
        self.__start_epoch = 1
        self.__checkpoint_path = ""
        self.__use_gpu = False

    def need_checkpoints(self, checkpoint_path: str):
        """
        Включает добавление чекпоинтов при обучении на каждой эпохе
        
        Чекпоинты сохраняются в формате checkpoint_{номер чекпоинта}.cpt

        Аргументы: 
            checkpoint_path: путь к директории, куда будут сохраняться файлы чекпоинтов
        
        Возвращает объект класса ModelTrainer
        """
        self.__make_checkpoints = True
        self.__checkpoint_path = checkpoint_path
        self.__create_dir(checkpoint_path)
        return self

    def __create_dir(self, path: str):
        if os.path.exists(path) == False:
            os.mkdir(path)

    def set_epochs(self, epoch_num: int):
        """
        Устанавливает количество эпох обучения

        Аргументы:
            epoch_num: количество эпох обучения

        Возвращает объект класса ModelTrainer
        """
        self.__epoch_num = epoch_num
        return self

    def start_training(self):
        """
        Начинает процесс обучения модели
        """

        if self.__start_epoch >= self.__epoch_num:
            raise ValueError("Start epoch should be less than epoch number")
        
        for epoch in range(self.__start_epoch, self.__epoch_num):
            self.__train_model(epoch+1)

    def __train_model(self, epoch: int):
        print(f"Start training, Epoch {epoch}")
        self.__model.train()
        current_loss = self.__proceed_pipeline()
        dataset_len = len(self.__dataloader.dataset) #type: ignore
        epoch_loss = current_loss / dataset_len
        if (self.__make_checkpoints):
            self.__make_checkpoint(epoch, epoch_loss)
        print(f'Epoch {epoch}/{self.__epoch_num}, Loss: {epoch_loss:.4f}')

    def __proceed_pipeline(self):
        current_running_loss = 0.0
        for images, depths in self.__dataloader:
            if self.__use_gpu:
                images, depths = self.__load_data_to_gpu(images, depths)
            outputs = self.__model(images)
            loss = self.__calculate_loss(outputs, depths)
            current_running_loss += loss.item() * images.size(0)
        return current_running_loss

    def __load_data_to_gpu(self, images, depths):
        return images.to(torch.device('cuda')), depths.to(torch.device('cuda'))

    
    def use_gpu(self):
        """
        Включает использование GPU при обучении

        Возвращает объект класса ModelTrainer
        """
        self.__use_gpu = True
        return self

    def __calculate_loss(self, outputs, depths):
        loss = self.__criterion(outputs, depths)                
        self.__optimizer.zero_grad()
        loss.backward()
        self.__optimizer.step()
        return loss

    def __make_checkpoint(self, epoch, loss):
        torch.save({
            'epoch': epoch,
            'model_state_dict': self.__model.state_dict(),
            'optimizer_state_dict': self.__optimizer.state_dict(),
            'loss': loss
        }, os.path.join(self.__checkpoint_path, f"checkpoint_{epoch}.cpt"))
        print("checkpoint was made")

    def save_model(self, save_path: str):
        """
        Сохраняет обученную модель в указанную директорию

        Аргументы:
            save_path: путь к директории, куда будет сохранена модель
        """
        self.__create_dir(save_path)
        model_scripted = torch.jit.script(self.__model)
        model_scripted.save(os.path.join(save_path, 'trained.pt'))

    def get_trained_model(self):
        """
        Возвращает обученную модель
        """
        return self.__model
    
    def load_from_checkpoint(self, epoch_num: int):
        """
        Продолжает обучение модели с чекпоинта

        Аргументы:
            epoch_num: количество эпох обучения

        Возвращает объект класса ModelTrainer
        """
        checkpoint_file_path = os.path.join(self.__checkpoint_path, f"checkpoint_{epoch_num}.cpt")

        if os.path.exists(checkpoint_file_path) == False:
            raise FileNotFoundError(f"Checkpoint file with epoch {epoch_num} not found")

        checkpoint = torch.load(self.__checkpoint_path)
        self.__start_epoch = checkpoint['epoch']
        self.__model.load_state_dict(checkpoint['model_state_dict'])
        self.__optimizer.load_state_dict(checkpoint['optimizer_state_dict'])
        self.set_epochs(epoch_num)
        self.start_training()
        return self
