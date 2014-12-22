###Introduction
Flask restful is an extension for Flask that adds support for quickly building a REST API's. 
The Flask Restful Framework is lightweight and works with existing libraries and encourages 
best practices with minimal setup. 

###Installation
Install Flask-Restful with pip
```
pip install flask-restful
```
See further installation directions at:
[flask-restful.readthedocs.org](http://flask-restful.readthedocs.org/en/latest/installation.html)

###A simple API using Flask Restful
This shows a very simple API using Flask Restful. This code should be sufficient to get you going.
```python
from flask import Flask
from flask.ext import restful
from flask import request
import json

app = Flask(__name__)
api = restful.Api(app)
name = "JohnnyRotten"


class NameAPI(restful.Resource):
  def get(self):
    return {'name': name}
  
  def post(self):
    data = json.loads(request.data)
    if data['name'] != None:
      name = data['name']
      return 'ok', 200
    else:
      return 'Bad request', 404

api.add_resource(NameAPI, '/api/name')

if __name__ == '__main__':
  app.run(debug=True)
```

###Trying it out
Trying out the API for the first time can be done with curl should return the name Johnny Rotten as shown below. 
```
$ curl http://127.0.0.1:5000/api/name
{"name": "JohnnyRotten"}
```

You can try out posting to your simple API by using curl
```
$ curl -H "Content-Type: application/json" -d '{"name":"SidVicious"}' http://127.0.0.1:5000/api/name
```
Should return 200 OK

If you now curl the API the name received should be SidVicious.
```
$ curl http://127.0.0.1:5000/api/name
{"name": "SidVicious"}
```

###Troubles
If the server is hanging on request it might be a good idea to add the the `threaded=True` to the `app.run()`

```python
app.run(threaded=True)
```

###Deployment
When the server is ready to be deployed to production it is necessary to wrap it into apache by using the mod_wgsi.
Instructions for using flask under Apache can be found at:
[http://flask.pocoo.org/docs/0.10/deploying/mod_wsgi/](http://flask.pocoo.org/docs/0.10/deploying/mod_wsgi/)

###Good tutorials for flask restful:
[http://blog.miguelgrinberg.com/post/designing-a-restful-api-with-python-and-flask](http://blog.miguelgrinberg.com/post/designing-a-restful-api-with-python-and-flask)

###Documentation
Flask documentation can be found at:
[http://flask.pocoo.org/docs/0.10/](http://flask.pocoo.org/docs/0.10/)

Flask restful documentation can be found at:
[http://flask-restful.readthedocs.org/en/latest/index.html](http://flask-restful.readthedocs.org/en/latest/index.html)

