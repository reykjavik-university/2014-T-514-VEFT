from db import Base
from sqlalchemy import event
from sqlalchemy import Column, Integer, String
from elasticsearch import Elasticsearch


class Entry(Base):
    __tablename__ = 'entries'
    id = Column(Integer, primary_key=True)
    title = Column(String)
    content = Column(String)


def after_entry_insert(mapper, connection, target):
    es = Elasticsearch()
    es.index(index="blog", doc_type="entry", id=target.id,
             body={"title": target.title, "content": target.content})


def after_entry_update(mapper, connection, target):
    pass

event.listen(Entry, 'after_insert', after_entry_insert)
event.listen(Entry, 'after_update', after_entry_insert)
