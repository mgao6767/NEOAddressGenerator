# NEO Address Generator
NEO Address Generator is a NEO vanity address generator. 
It generates the NEO address and corresponding private key of the address. 
***Please keep your private key secure!***

## Requirements currently support:

* **StartWith**: The address generated starts with certain characters.
* **EndWith**: The address generated ends with certain characters.
* **Contains**: The address generated contains certain characters.
* **Uppercase**: The address generated contains less than specified number of uppercase characters.
* **Length**: The address generated is shorter than specified length.

## Usage:

1. Simply enter your desired characters in the .txt files and run the program. 

2. Enable what requirements you want and start generating.

## Notice:
 
* To prevent freezing, the generator by default uses N-1 cores, where N is the number of CPU cores. 
The generator uses at most 7 cores.

## Disclaimer:

This generator is written by [chenzhitong](https://github.com/chenzhitong), who is one core developer of NEO,
and the source code is from [AntsharesTools](https://github.com/chenzhitong/AntsharesTools).

The generator does not record any address or key generated. 
If you doubt it, then have someone review the code or don't use it.