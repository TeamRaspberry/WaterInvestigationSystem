import torch
import torch.nn as nn
import torch.nn.functional as F

# Определение резидуального блока
class ResidualBlock(nn.Module):
    def __init__(self, in_channels, out_channels):
        super(ResidualBlock, self).__init__()
        self.conv1 = nn.Conv2d(in_channels, out_channels, kernel_size=3, padding=1)
        self.bn1 = nn.BatchNorm2d(out_channels)
        self.conv2 = nn.Conv2d(out_channels, out_channels, kernel_size=3, padding=1)
        self.bn2 = nn.BatchNorm2d(out_channels)

        # Условие для проекции, если число каналов меняется
        self.projection = nn.Conv2d(in_channels, out_channels, kernel_size=1) if in_channels != out_channels else None

    def forward(self, x):
        shortcut = x
        if self.projection:
            shortcut = self.projection(shortcut)

        x = F.relu(self.bn1(self.conv1(x)))
        x = self.bn2(self.conv2(x))
        return F.relu(x + shortcut)

# Определение блока энкодера
class EncoderBlock(nn.Module):
    def __init__(self, in_channels, out_channels):
        super(EncoderBlock, self).__init__()
        self.res_block = ResidualBlock(in_channels, out_channels)
        self.pool = nn.MaxPool2d(kernel_size=2, stride=2)

    def forward(self, x):
        skip_connection = self.res_block(x)
        x = self.pool(skip_connection)
        return x, skip_connection

# Определение блока декодера
class DecoderBlock(nn.Module):
    def __init__(self, in_channels, out_channels):
        super(DecoderBlock, self).__init__()
        self.upconv = nn.ConvTranspose2d(in_channels, out_channels, kernel_size=2, stride=2)
        self.res_block = ResidualBlock(in_channels, out_channels)

    def forward(self, x, skip_connection):
        x = self.upconv(x)
        # Слияние с соответствующим скип-соединением
        x = torch.cat([x, skip_connection], dim=1)
        x = self.res_block(x)
        return x

# Полная архитектура Res-UNet
class ResUNet(nn.Module):
    def __init__(self, input_channels=3, output_channels=1):
        super(ResUNet, self).__init__()

        # Энкодер
        self.enc1 = EncoderBlock(input_channels, 64)
        self.enc2 = EncoderBlock(64, 128)
        self.enc3 = EncoderBlock(128, 256)
        self.enc4 = EncoderBlock(256, 512)

        # Боттлнек
        self.bottleneck = ResidualBlock(512, 1024)

        # Декодер
        self.dec4 = DecoderBlock(1024, 512)
        self.dec3 = DecoderBlock(512, 256)
        self.dec2 = DecoderBlock(256, 128)
        self.dec1 = DecoderBlock(128, 64)

        # Выходной слой
        self.output_conv = nn.Conv2d(64, output_channels, kernel_size=1)

    def forward(self, x):
        # Проход через энкодер
        x, skip1 = self.enc1(x)
        x, skip2 = self.enc2(x)
        x, skip3 = self.enc3(x)
        x, skip4 = self.enc4(x)

        # Боттлнек
        x = self.bottleneck(x)

        # Проход через декодер
        x = self.dec4(x, skip4)
        x = self.dec3(x, skip3)
        x = self.dec2(x, skip2)
        x = self.dec1(x, skip1)

        # Выходной слой
        x = self.output_conv(x)
        return x