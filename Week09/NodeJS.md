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

Node.js has a command line shell.  Also called nodejs terminal or [REPL](http://nodejs.org/api/repl.html).  To activate it execute the following command in terminal

	node

and the prompt in terminal should change (something like this)

```nodejs
☁  ~  node
>
```

Inside this shell you can write javascript code.  Here below we write a simple function that takes in a number and doubles it.

```javascript
> function double(value){return value * 2;}
```
Then we run our function in the nodejs shell be executing the command

```javascript
> double(2.3)
```

And the terminal window will show

```nodejs
> function double(value){return value * 2;}
undefined
> double(2.3)
4.6
> 
```

Node.js shell is a interactive javascript shell that is mainly used to test statments.  The main power of node.js is when we write a javascript file and run it with node.js.  Lets start with writing a very simple __helloWorld.js__ file and run it with node.js.  Open your favorite text editor and write the following.

```javascript
function helloworld() {
	console.log('Hello World!');
}

helloworld();
```

Now save this file as **helloWorld.js** on your computer (in any directory you like).

Lets run **helloWorld.js** from a terminal window.  You open the terminal window and go to the directory where the file is saved.  In my case I made a directory on my desktop called `nodejsTest`, so in my terminal window I write `cd ~desktop/nodejsTest/` and execute it.
To run the file with node.js you execute

```bash
node helloWorld.js
```
Your terminal window should show something very simular to this.
```bash
☁  nodejsTest   cd ~
☁  ~  cd ~/Desktop/nodejsTest
☁  nodejsTest  node helloWorld.js
Hello World!
☁  nodejsTest  
```

TODO

node shell

more on npm?