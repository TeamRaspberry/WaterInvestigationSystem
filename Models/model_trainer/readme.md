# ModelTrainer

Класс предназначен для тренировки модели PyTorch

## Интерфейс

### need_checkpoints(checkpoint_path: str):
            
Включает добавление чекпоинтов при обучении на каждой эпохе
            
Чекпоинты сохраняются в формате checkpoint_{номер чекпоинта}.cpt

Аргументы: 
checkpoint_path: путь к директории, куда будут сохраняться файлы чекпоинтов

### set_epochs(epoch_num: int):

Устанавливает количество эпох обучения

Аргументы:
epoch_num: количество эпох обучения

Возвращает объект класса ModelTrainer

### start_training():

Начинает процесс обучения модели

### use_gpu():

Включает использование GPU при обучении

Возвращает объект класса ModelTrainer

### save_model(save_path: str):

Сохраняет обученную модель в указанную директорию

Аргументы:save_path: путь к директории, куда будет сохранена модель

### get_trained_model():

Возвращает обученную модель

### load_from_checkpoint(epoch_num: int):

Продолжает обучение модели с чекпоинта

Аргументы:
epoch_num: количество эпох обучения

Возвращает объект класса ModelTrainer

## Пример использования

Создание тренера с определённым количеством эпох, использованием чекпоинтов и обучением на GPU

```
def create_trainer(model, train_data, checkpoint_path: None|str=None, epochs=10):
    trainer = ModelTrainer(model, train_data)
    trainer = trainer.set_epochs(epochs)
    trainer = trainer.use_gpu()
    if checkpoint_path:
        trainer = trainer.need_checkpoints(f"{checkpoint_path}/checkpoints")
    return trainer
```
