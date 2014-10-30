# Setting up Python developer environment on OS X
This document contains a description on setting up a Python developer
environment on OS X. This document has been tested on OS X 10.9.5. If you find
errors or if you find anything missing, please send in a pull request with your
changes. All pull requests are well accepted.

There are many great tools that can help you writing awesome Python code. The
tools that are mentioned in this documents are the ones I use daily when
working with Python code.

## Installing Homebrew
If you have used other Unix-like operating systems, such as Linux or BSD, you
must have used some kind of package manager for installing software. If you
have used OS X for some time you might have noticed that such functionality is 
completely missing. There are several projects out there which solve this
problem. My favorite one, that I trust and love is [Homebrew](http://brew.sh/).
They even use the subtitle *"The missing package manager for OS X"*.

To install Homebrew you first need to install Xcode and the Xcode command line
tools. The Xcode command line tool contains various of Unix tools which Brew
depend on, such as Git.

If you don't have Xcode you must start by installing it. It might take a while,
since Xcode is a big application (2.46 GB last time i checked). To install
Xcode, open up the App Store application that comes with OS X, search for Xcode and
click the *Free* button.

When Xcode is installed, you need to open it and accept a million line licence term before
you can use the Xcode command line tools. Open up a terminal and type in

	sudo git

Type in your password, and agree to the licence terms. After that commands like
`git` and `gcc` should be available in your terminal.

Next we install Homebrew. Open up a terminal and type in the following.

	ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"

If you are the kind of person that is not willing to run scripts without reading them, well
in this case it should be safe. I trust the Homebrew guys. If not, read through
the scripts before you execute them in your shell.
	
When the install finishes, run 

	brew doctor

If everything worked as planned you should get a message from Brew doctor
stating that your system is ready to brew. If not, Brew doctor will tell you
what went wrong and try to help you fix it.

We know use Brew to install Python and other Python utilities.

## Install Python
Python 2.7 is installed by default on OS X but the Python shared library and
the library headers are missing. Many python packages which are not written in
Python depend on these things.

To install Python and with shared objects and header files, open up your
terminal and ask Brew to install it for us.

	brew install python --framework

If you do `brew list` you can see all packages (most often referred to as
formulae in brew) that have been installed with Brew.

## Pip and virtualenv
Pip is a package management system used to install and manage software packages
written in Python. Many packages can be found in the Python Package Index
[PyPI](https://pypi.python.org/pypi) but packages can be installed directly
from Github or from local packages on your machine.

With Pip we can install external Python packages that we need in our Python
projects. To name a few, these can be Flask, Django or SQLAlchemy.

When we installed Python using Brew in the section above we installed Pip as
well. We use Pip heavily when we are writing our Python projects in this course
so be sure to install it properly.

Next we install Python virtualenv. This one is my favourite and I can't
understand how I was able to live without it couple of years ago. Virtualenv
allows you to create a virtual environment, put simply, an isolated working
copy of Python which allows you to work on a specific project without worry of
affecting other projects. For example, if you are working on two different
projects with different dependencies you can install them into the virtualenv
without installing it into the global Python installation on your machine.
Brilliant. When the virtualenv is active you can use all the packages that
were installed into that environment. When you are done, you remove the
environment without leaving any trace of files on your hard drive.

To install virtualenv, open up your terminal and type in the following.

	sudo pip install virtualenv

Before you continue, take a brew moment and Google Pip and virtualenv and read
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

Create a folder somewhere on your disk, I used ~/Desktop/movies. You can do the
same.

	➜  mkdir ~/Desktop/cinema
	➜  cd ~/Desktop/cinema
	
In that folder we will create a Python virtualenv (under the folder .venv) for
the external packages that we depend on.

	➜  cinema  virtualenv .venv
	New python executable in .venv/bin/python2.7
	Also creating executable in .venv/bin/python
	Installing setuptools, pip...done.
	
Next we enable the virtualenv in our shell. Note that you are only activating
the virtualenv in the shell that you are using. If you open up a new one you
must enable the virtualenv for that shell as well.

	➜  cinema  source .venv/bin/activate
	(.venv)➜  cinema

Next we install the two above mentioned packages into our virtualenv. Pip
packages can be install with `pip install`. You can also search on Pypi using
`pip search`.


	(.venv)➜  cinema  pip install requests
	Downloading/unpacking requests
	  Downloading requests-2.4.1-py2.py3-none-any.whl (458kB): 458kB downloaded
	Installing collected packages: requests
	Successfully installed requests
	Cleaning up...

	(.venv)➜  cinema  pip install termcolor
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
	    # Do a get request to cinema api at apis.is
	    response = requests.get('http://apis.is/cinema')
	
	    # If we don't receive a 2xx status code, we stop and print error.
	    if not response.ok:
	        print 'Unable to read from api, got non ok status code: {0}'\
	            .format(response.status_code)
	        sys.exit(1)
	
	    # The response is on json format. We use python json module to
	    # parse the json
	    # and map it into a Python associated array (dictionary)
	    d = json.loads(response.text)
	
	    # We loop through the result and print it to stdout.
	    for res in d.get('results'):
	        print colored(res.get('title').encode('utf-8'), 'yellow')
	        for showtime in res.get('showtimes'):
	            print ' ', showtime.get('theater').encode('utf-8')
	            for schedule in showtime.get('schedule'):
	                print '  ', schedule.encode('utf-8')
	
	if __name__ == '__main__':
	    show_running_movies()



Exit your editor and run the script.

	(.venv)➜  cinema  python cinema.py
	Afinn
	  Sambíóin Álfabakka
	   15:20 (1)
	   15:20 (P)
	   17:40 (1)	
	   ...

If everything went well the script should print out the movies that are being
shown. Nice, ey?

If you are new to Python please stop and read through the code and convince
yourself that it does what it is meant to do. The author of the Python language
states that the language is so simple that it is almost identical to natural
languages. I don't agree. But simple well written Python code can be that way;
and most often in large Python code base things can be quite tricky. But that
is my rant. Don't take my word for it.

You should now have everything setup for writing Python code on your Mac.
