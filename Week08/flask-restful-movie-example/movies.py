movies_db = []

class Movie(object):
    def __init__(self, name, year):
        self.name = name
        self.year = year

    def to_dict(self):
        return {'name': self.name, 'year': self.year}

m1 = Movie('The hackes', 1995)
m2 = Movie('The Matix', 1999)
movies_db.append(m1)
movies_db.append(m2)
