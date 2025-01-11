from dataset_loader.SeathruDataset import SeathruDataset
from torchvision import transforms
from model.ResUNet import ResUNet
from model_trainer.ModelTrainer import ModelTrainer
from type_annotations.dataset_route import dataset_route
from pathlib import Path
from torch.utils.data import random_split
import os

def main():
    transform = create_transform()
    datasets: list[dataset_route] = load_datasets()
    transformed_dataset = SeathruDataset(datasets, transform=transform)
    train_data, validate_data = split_dataset(dataset=transformed_dataset)
    model = create_model()
    checkpoint_path = os.path.join(Path(__file__).parents[1], "checkpoints")
    trainer = create_trainer(model, train_data, checkpoint_path=checkpoint_path)
    trainer.start_training()
    weights_path = os.path.join(Path(__file__).parents[1], "weights")
    trainer.save_model(weights_path)

def load_datasets():
    path = Path(__file__).parents[2]
    datasets: list[dataset_route] = [
        (f"{path}/datasets/D1/D1/linearPNG", f"{path}/datasets/D1/D1/depth"),
        (f"{path}/datasets/D2/D2/linearPNG", f"{path}/datasets/D2/D2/depth"),
        (f"{path}/datasets/D3/D3/linearPNG", f"{path}/datasets/D3/D3/depth"),
        (f"{path}/datasets/D4/D4/linearPNG", f"{path}/datasets/D4/D4/depth_resized"),
        (f"{path}/datasets/D5/D5/linearPNG", f"{path}/datasets/D5/D5/depth")
    ]
    return datasets

def split_dataset(dataset, split=[0.7, 0.3]):
    return random_split(dataset, split)

def create_model():
    return ResUNet(3, 1).to('cuda')

def create_transform():
    return transforms.Compose([
        transforms.Resize((128, 128)),
        transforms.ToTensor()
    ])

def create_trainer(model, train_data, checkpoint_path: None|str=None, epochs=10):
    trainer = ModelTrainer(model, train_data)
    trainer = trainer.set_epochs(epochs)
    if checkpoint_path:
        trainer = trainer.need_checkpoints(f"{checkpoint_path}/checkpoints")
    return trainer

if __name__ == "__main__":
    main()