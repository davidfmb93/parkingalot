# Parking A Lot

## Install

You should have installed

* Docker
* Docker Compose

You have differents alternatives to install the program:

1. You can execute the following lines:
```sh
bash install.sh #first time
bash start.sh  #When you need running the program again
```

2. You can install program using The Makefile but you should install  [chocolatey](https://chocolatey.org/install). After that, run in command line: 

```sh
make INSTALL #first time
make RUN #When you need running the program again
```

3. You can lunch the program using docker-compose commands

When you run the previous commands you can go to the following link http://localhost:7201/swagger"

## Membership
- 0 => Oficial
- 1 => Resident
- 2 => NoResident

## Uses Cases

1. "Login" => POST /check-in
2. "LogOut" => POST ​/check-out​/{NumberPlate}
3. "Change membership" : 
    * Oficial[0] => PUT /api/vehicle/rol/0/{NumberPlate}
    * Resident[1] => PUT /api/vehicle/rol/1/{NumberPlate}
    * NoResident[2] => PUT /api/vehicle/rol/2/{NumberPlate}
4. Start Month => ​GET /api​/vehicle​/restart
5. Resident Payments => GET ​/api​/vehicle​/report

## Connection DB

```
Server: localhost, 1433
Authentication type: SQL Login
user name: sa
Password: S3cur3P@ssW0rd!
```

### Diagram DB

![Diagram DB](https://drive.google.com/uc?id=1kUHTsuwqcxZzafOUsQ_YBg8V8dmCX4uB)


## UML

<img src="https://drive.google.com/uc?id=1QqzYbmU_YxQwiD2EerHkhinipTIX1rPo" data-canonical-src="https://drive.google.com/uc?id=1QqzYbmU_YxQwiD2EerHkhinipTIX1rPo" width="500" />

## More Information

Contact: 
Email: davidfmb93@gmail.com
Github: https://github.com/davidfmb93