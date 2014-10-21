from flask.ext.script import Manager
from db import Base, engine

import models

from app import app

manager = Manager(app)


@manager.command
def create_db():
    Base.metadata.create_all(engine)


@manager.command
def debug():
    app.run(debug=True)

if __name__ == "__main__":
    manager.run()
