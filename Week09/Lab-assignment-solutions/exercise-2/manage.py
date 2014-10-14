# manage.py

from flask.ext.script import Manager

from app import app
from db import engine, Base
from models import Movie

manager = Manager(app)


@manager.command
def create_db():
    Base.metadata.create_all(engine)


@manager.command
def debug():
    app.run(debug=True)

if __name__ == "__main__":
    manager.run()
