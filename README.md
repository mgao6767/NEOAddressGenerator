# NEO Address Generator
NEO Address Generator is a NEO vanity address generator. 
It generates the NEO address and corresponding private key of the address.

***Please keep your private key secure!***

## Requirements currently support:

* **StartWith**: Generate address that starts with certain characters.
* **EndWith**: Generate address that ends with certain characters.
* **Contains**: Generate address that contains certain characters.
* **LessUppercase**: Generate address that contains less than specified number of uppercase characters.
* **ShortLength**: Generate address that is shorter than specified length (Not the number of characters in the address string, but the display length).

## Simple UI

The generator offers a simple and easy-to-understand UI, like the following where StartWith is checked.

![Main View](/mainview.jpg)

## Usage:

1. Simply enter your desired characters in the .txt files and run the program.

2. Enable the requirements you want, check whether these requirements should be met simultaneously and start generating.

3. Copy the private key of your desired address and open your NEO-GUI, the official wallet, select ```import from WIF``` 
and paste the private key there.
The wallet will automatically include the address as a standard account.

## Notice:
 
* To prevent freezing, the generator by default uses N-1 cores, where N is the number of CPU cores. 
The generator uses at most 7 cores.

* Complicated requirements take time. The longer requirements you have, the longer time (exponentially) it takes.
	For example, a StartWith requirement of "ABC" may cost less than 1min to generate one address, but "ABCD" may cost several minutes.

## Disclaimer:

This generator is initially written by [chenzhitong](https://github.com/chenzhitong), who is one core developer of NEO,
and the source code is from [AntsharesTools](https://github.com/chenzhitong/AntsharesTools).

I've modified the code so as to make it more user friendly and allow for more customized combinations of vanity address requirements.

The generator does not record any address or key generated. 
If you doubt it, then have someone review the code or don't use it.