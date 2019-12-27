# Maze USB Spreading Trick
 USB Spreading Trick
 
Youtube :

https://youtu.be/2iAt-j7Bxnc

![gui](https://raw.githubusercontent.com/MagicianMido32/Maze-USB-Spreading-Trick/master/1.PNG)


 

When the victim launch your malware for the first time 

This malware will be a first stage downloader, 

It will download two things, your spyware and this spreader file,

The first stage downloader will copy itself into a path in the victims pc e.g. %appdata%\Encrypted.exe

The spyware will work separately from this file.

When the victim inserts a usb into his machine

This spreader will hide all the victims' files inside a maze of folders

It will then copy the first stage downloader into the usb inside the maze

It will also drop a spreading artifact that has the same icon as the usb to trick the victim

When the victim opens his usb he will see nothing but system_volume_information file (if he enabled show hidden files)

As well as a file without an obvious extension that looks exactly like his usb

The trick here is that the victim will launch this file thinking it's his usb, but it's nothing by our spreading artifact in disguise

This artifact will then launch our first stage downloader again, as well as the victim's files and the loop goes on.



the idea here is that most usb spreading techniques available out there depend on directly binding the victim's files into exe , which will not only corrupt his files but will also get detected easily by Avs (direct exe binding is widely detected)

But here we don't use any kind of binding and the victim's files stay the same

although it's possible for the victim to find his files easily through search, but our idea depends on him not realizing the infection in the first place, It looks too true to trick most users to launch that spreading artifact file that looks exactly like the usb and it's extension isn't appearing (because of another trick of making the name too long by adding spaces)

Also if the victim realized that the usb is infected he won't be tricked by other more-suspicious usb infection tricks.

It's possible however to add a simple ransomware that encrypts the victims files while infecting the usb and decrypt them when the spreading artifact is launched on another machine.

This method is extremely effective while pentesting organization that has no inter-network between its pcs and rely only on usb thumb stick to share files together (lost of organizations actually) 

