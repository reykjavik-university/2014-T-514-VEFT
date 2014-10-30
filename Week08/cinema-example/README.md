# Cinema example
This code is used as example in the Linux and OS X Python environment setup.

## Install

Create a virtualenv and activate it.

	virtualenv .
	source bin/activate

Next install the requirements, created with `pip freeze`. When working with virtualenv, you can freeze the environment and write it into a file. This file can the be read by pip for reconstructing the environment again.

	pip install -r requirements.txt

Then you can simply run the cinema script with

	python cinema.py