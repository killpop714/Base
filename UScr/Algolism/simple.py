class Player:
    def __init__(self,name,hp,attack):
        self.name = name
        self.hp = hp
        self.attack = attack

    def hit(self,target):
        target.hp -= self.attack