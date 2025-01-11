from torch.utils.data import Dataset
from type_annotations.dataset_route import dataset_route
import os
from PIL import Image

class SeathruDataset(Dataset):

    def __init__(self, data_routes: list[dataset_route], transform=None):

        self.transform = transform

        self.image_pathes: list[str] = []
        self.depth_pathes: list[str] = []

        for route in data_routes:

            image_dir = route[0]
            depth_dir = route[1]

            self._parse_dataset_directories(image_dir, depth_dir)

    def _parse_dataset_directories(self, image_dir: str, depth_dir: str):

        image_names: list[str] = os.listdir(image_dir)
        depth_names: list[str] = os.listdir(depth_dir)

        for image_name in image_names:

            image_name_without_format: str = image_name.split('.')[0]

            related_depth: str = f"depth{image_name_without_format}.tif"

            if (related_depth in depth_names):

                image_full_path = os.path.join(image_dir, image_name)
                depth_full_path = os.path.join(depth_dir, related_depth)

                self.image_pathes.append(image_full_path)
                self.depth_pathes.append(depth_full_path)                

    def __getitem__(self, index):
        
        image = Image.open(self.image_pathes[index])
        depth = Image.open(self.depth_pathes[index])

        if self.transform:
            image = self.transform(image)
            depth = self.transform(depth)
        
        return image, depth       

    def __len__(self):
        return len(self.image_pathes) 