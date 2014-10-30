# Setting up Python developer environment on Ubuntu
This document contains a description on setting up a Python developer
environment on Linux Ubuntu. This document was tested on Ubuntu Trusty 14.04. If you
find errors or if you find anything missing, please send in a pull request with
your changes. All pull requests are well accepted.

There are many great tools that can help you writing awesome Python code. The
tools that are mentioned in this documents are the once I use daily when
working with Python code.


## Install Python
Python 2.7 is pre-installed when you install Ubuntu but the
Python shared library and the library headers are missing. Many python packages
which are not written in Python depend on these things.

Before, let us install the famous build essentinal package which includes the C/C++ compilers from GNU. Open up your terminal and type in the following.

    ~$ sudo apt-get install build-essential

Next we install the python development package which installs the Python shared library and header files.

    ~$ sudo apt-get install python-dev


## Pip and virtualenv
Pip is a package management system used to install and manage software packages
written in Python. Many packages can be found in the Python Package Index
[PyPI](https://pypi.python.org/pypi) but packages can be installed directly
from Github or from local packages on your machine.

With Pip we can install external Python packages that we need in our Python
projects. To name a few, these can be Flask, Django or SQLAlchemy.

    ~$ sudo apt-get install python-pip

We use Pip heavily when we are writing our Python projects in this course so be
sure to install it properly.

Next we install Python virtualenv. This one is my favourite and I can't
understand how I was able to live without it couple of years ago.  Virtualenv
allows you to create a virtual environment, put simply, an isolated working
copy of Python which allows you to work on a specific project without worry of
affecting other projects. For example, if you are working on two different
projects with different dependencies you can install them into the virtualenv
without installing it into the global Python installation on your machine.
Brilliant.  When the virtualenv is active you can use all the packages that
were installed into that environment. When you are done, you remove the
environment without leaving any trace of files on your hard drive.

To install virtualenv, open up your terminal and type in the following.

	~$ sudo apt-get install python-virtualenv
	
Before you continue; take a brew moment and Google Pip and virtualenv and read
more about them. They will make your life so much easier in the future. I
recommend this
[blog](http://www.dabapps.com/blog/introduction-to-pip-and-virtualenv-python/)
post by Jamie Matthews to start with.


## Let's take our environment for a spin drive
Now let's take our new environment for a spin and write a small application
that prints out to the stdout the movies that are being shown in the Icelandic
theatres. We use the apis.is/cinema API. Be sure to give them some follow up on
their [Github](https://github.com/kristjanmik/apis) page. Great API that you
can use in your applications.

Our application will depend on two Python packages:

- [requests](http://docs.python-requests.org/en/latest/): Nice package for crafting web requests.
- [termcolor](https://pypi.python.org/pypi/termcolor): For printing out on stdout in colours.

Create folder somewhere on your disk, I used ~/Desktop/movies. You can do the
same.

	~$ mkdir ~/Desktop/cinema
	~$ cd ~/Desktop/cinema
	
In that folder we will create a Python virtualenv (under the folder .venv) for
the external packages that we depend on.

	~/Desktop/cinema$ virtualenv .venv
	New python executable in .venv/bin/python2.7
	Also creating executable in .venv/bin/python
	Installing setuptools, pip...done.
	
Next we enable the virtualenv in our shell. Note that you are only activating
the virtualenv in the shell that you are using. If you open up a new one you
must enable the virtualenv for that shell as well.

	~/Desktop/cinema$ source .venv/bin/activate
	(.venv)~/Desktop/cinema$ 

Next we install the two above mentioned packages into our virtualenv. Pip
packages can be install with `pip install`. You can also search on Pypi using
`pip search`.


	(.venv)~/Desktop/cinema$ pip install requests
	Downloading/unpacking requests
	  Downloading requests-2.4.1-py2.py3-none-any.whl (458kB): 458kB downloaded
	Installing collected packages: requests
	Successfully installed requests
	Cleaning up...

	(.venv)~/Desktop/cinema$ pip install termcolor
	Downloading/unpacking termcolor
	  Downloading termcolor-1.1.0.tar.gz
	  Running setup.py (path:/Users/hlysig/Desktop/cinema/.venv/build/termcolor/setup.py) egg_info for package termcolor
	
	Installing collected packages: termcolor
	  Running setup.py install for termcolor
	
	Successfully installed termcolor
	Cleaning up...

Now open up your favourite text editor and type (or paste) the following code
into file named cinema.py:


	import json
	import sys
	
	import requests
	from termcolor import colored
	
	
	def show_running_movies():
	    """
	    Fetches movies that are beeing shown in Icelandic theaters and
	    prints them out to stdout
	    """
	    # Do a get request to cinema api at api.is
	    response = requests.get('http://apis.is/cinema')
	
	    # If we don't receive a 2xx status code, we stop and print error.
	    if not response.ok:
	        print 'Unable to read from api, got non ok status code: {0}'\
	            .format(response.status_code)
	        sys.exit(1)
	
	    # The respond is on json format. We use python json module to
	    # parse the json
	    # and map it into a Python associated array (dictionary)
	    d = json.loads(response.text)
	
	    # We loop the result and print it to stdout.
	    for res in d.get('results'):
	        print colored(res.get('title').encode('utf-8'), 'yellow')
	        for showtime in res.get('showtimes'):
	            print ' ', showtime.get('theater').encode('utf-8')
	            for schedule in showtime.get('schedule'):
	                print '  ', schedule.encode('utf-8')
	
	if __name__ == '__main__':
	    show_running_movies()



Exit your editor and run the script.

	(.venv)~/Desktop/cinema$ python cinema.py
	Afinn
	  Sambíóin Álfabakka
	   15:20 (1)
	   15:20 (P)
	   17:40 (1)	
	   ...

If everything went well the script should print out the movies that are being
shown. Nice ey?

If you are new to Python please stop and read through the code and convince
yourself that it does what it is meant to do. The author of the Python language
state that the language is so simple that it is almost identical to natural
languages. I don't agree. But simple well written Python code can be that way;
but most often in large python code base things can be quite tricky. But that
is my rant. Don't take my word for it.

You should now have everything setup for writing Python code on your Linux.
