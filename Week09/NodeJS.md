>Before we begin I want that to be noted that the following examples have not been tested in __linux__.  It is written in the best of my knowledge.  For example how to download node.js on linux.  It would be good if someone using linux would confirm that the following (download and examples) works on linux.


# Node.js

[Node.js](http://nodejs.org/) is an open source, cross-platform runtime environment for server-side and networking applications. Node.js applications are written in JavaScript, and can be run within the Node.js runtime on OS X, Microsoft Windows, Linux and FreeBSD.  You can read more about node.js on node.js homepage [http://nodejs.org/](http://nodejs.org/) and on [wikipedia](http://en.wikipedia.org/wiki/Node.js).

## How to install Node.js

To install node.js on OSX, open a terminal window and type

	brew install node

*Remember it is a good idea to do __brew update__ before you do brew install to ensure your Homebrew is up to date.

on linux, type in terminal

	sudo apt-get install nodejs

*It is a good idea to do **sudo apt-get update** before you do sudo apt-get install to ensure your apt-get is up to date.*

[npm](https://www.npmjs.org/) is node.js package manager.  The abbrebiate npm stands for Node Packaged Modules but often talked about node package manager.  npm is simular to pip in python.

 [npm](https://www.npmjs.org/) installs automatically on OSX when __brew install node__ is executed.  But on linux it must be install separately by executing the following command in Terminal window.

	sudo apt-get install nodejs npm

## Nodejs shell

Node.js has a command line shell.  To activate it execute the following command in terminal

	node

and the prompt in terminal should change to 

'''javascript
☁  ~  node
>
'''





TODO
node shell
more on npm?