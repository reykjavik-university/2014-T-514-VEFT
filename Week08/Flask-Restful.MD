###Introduction
Flask restful is an extension for Flask that adds support for quickly building a REST API's. 
The Flask Restful Framwork is lightweight and works with exixsting libraries and encorages 
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

api.add_resource(NameAPI, '/api/name/')

if __name__ == '__main__':
  app.run(debug=True)
```

###Trying it out
Trying out the API for the first time can be done with curl should return the name JohhnyRotten as shown below. 
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
#Deployment

It is recommended to do this through a virtual python instance (venv), details on how to set up a virtual environment for python can be found [here] [1].

###Install mod_wsgi

On Ubuntu/Debian enter the following in terminal:

    $ apt-get install libapache2-mod-wsgi

###Create .wsgi file
Next step is to create a .wsgi file which you need to run your application. For most basic application this should do:


    from smuu import app as application

    
If your application is a singleton you can directly import that one as application.

Save the .wsgi file somewhere you are likely to find it again, since you will be referring to it in a while. For example : "/var/www/smuuu". 
 Also install your application in the virtual environment.
 
 ###Configure Apache
 
 Now we need to create an Apache configuration file for your application. In this config we are running the application under a different user for "security reasons". This configuration only works under linux.
 
    <Virutalhost *>
        ServerName dabs.com
        
        WSGIDaemonProcess smuu user=user1 group=group1 threads=5
        WSGIScriptAlias / /var/www/smuu/smuu.wsgi
        
        <Directory /var/www/smuu>
            WSGIProcessGroup smuu
            WSGIApplicationGroup %{GLOBAL}
            Order deny,allow
            Allow from all
        </Directory>
    </VirtualHost>
 
 For more details check out the [mod_wsgi wiki] [2]
 
 ###Trouble in paradise
 
 If nothing happens and you sit in your chair smashing your head against the keyboard, try the following countermeasures before throwing your computer through the window:
 
#####Nothing happens, errorlog shows SystemExit ignored.
You probably have a app.run() call in the applicaiton file which is not guarded by

    if __name__ == '__main__':
    
Delete app.run(), or put it into a if block as shown above.
 
#####Application gives permission errors
App is being run by a wrong user, check if the app has privileges for all folders and that the app runs as a correct user.

#####Application dies with an error on print
mod_wsgi does not allow any usages of sys.stdout or sys.stderr. You can bypass this by adding this to the .wsgi config file:

    WSGIRestrictStdout Off
    
Or replace the standard out with a different stream in the .wsgi config file:

    import sys
    sys.stdout = sys.stderr
    
#####Accessing resources gives IO errors
Is your application a in a single .py file? Convert your application into a package.

[1]:https://github.com/reykjavik-university/2014-T-514-VEFT/blob/master/Week08/Python_environment_install_Linux.md
[2]:http://code.google.com/p/modwsgi/wiki/

###Good tutorials for flask restful:
[http://blog.miguelgrinberg.com/post/designing-a-restful-api-with-python-and-flask](http://blog.miguelgrinberg.com/post/designing-a-restful-api-with-python-and-flask)

###Documentation
Flask documentation can be found at:
[http://flask.pocoo.org/docs/0.10/](http://flask.pocoo.org/docs/0.10/)

Flask restful documentation can be found at:
[http://flask-restful.readthedocs.org/en/latest/index.html](http://flask-restful.readthedocs.org/en/latest/index.html)
