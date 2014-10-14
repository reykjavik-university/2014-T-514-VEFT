# Solution to exercise 2 in week 9

	virtualenv .venv
	source .venv/bin/activate
	pip install -r requirements.txt
	
To generate database

	python manage.py sync_db

to start web server 

	python manage.py runserver
	
Manage.py is written with nice flask extensions called [flask-script](http://flask-script.readthedocs.org/en/latest/). Check it out :)