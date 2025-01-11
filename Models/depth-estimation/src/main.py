from dataset_loader.DatasetLoader import SeathruDataset
from torchvision import transforms
from model.ResUNet import ResUNet
from model_trainer.ModelTrainer import ModelTrainer
from type_annotations.dataset_route import dataset_route
from pathlib import Path

def main():
    transform = transforms.Compose([
        transforms.Resize((256, 256)),
        transforms.ToTensor()
    ])

    path = Path(__file__).parents[2]

    datasets: list[dataset_route] = [
        (f"{path}/datasets/D1/D1/linearPNG", f"{path}/datasets/D1/D1/depth"),
        (f"{path}/datasets/D2/D2/linearPNG", f"{path}/datasets/D2/D2/depth"),
        (f"{path}/datasets/D3/D3/linearPNG", f"{path}/datasets/D3/D3/depth"),
        (f"{path}/datasets/D4/D4/linearPNG", f"{path}/datasets/D4/D4/depth_resized"),
        (f"{path}/datasets/D5/D5/linearPNG", f"{path}/datasets/D5/D5/depth")
    ]

    path = Path(__file__).parents[1]

    dataset = SeathruDataset(datasets, transform=transform)
    model = ResUNet().to('cuda')
    trainer = ModelTrainer(model, dataset).set_epochs(10).need_checkpoints(f"{path}/checkpoints")

    trainer.start_training()

    trainer.save_model(f"{path}/weights")

if __name__ == "__main__":
    main()