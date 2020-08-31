import { Car } from './car';

export class FakeData {
  public cars: Car[] = [
    { id: 1, brand: "Audi", model: "RS6", engine: "V10 5.2", power: 580, production: "2012-01-02T00:00:00", price: 120000, imagePath: "https://apollo-ireland.akamaized.net/v1/files/eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE/image;s=1080x720;cars_;/937787999_;slot=10;filename=eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE_rev001.jpg",
       description: "Autko w super stanie! Garażowane! Od fanatyka!", mileage: 130000 },
    { id: 2, brand: "Audi", model: "R8", engine: "V10 5.2", power: 525, production: "2010-03-24T00:00:00", price: 320000, imagePath: "http://blog.ozonee.pl/wp-content/uploads/2018/07/Audi-R8-V10.jpg",
       description: "Niestety koszty utrzymania samochodu to fanaberia!", mileage: 54000 },
    { id: 4, brand: "Mercedes", model: "A 180", engine: "1.6", power: 122, production: "2012-06-22T00:00:00", price: 49500, imagePath: "https://ireland.apollo.olxcdn.com/v1/files/eyJmbiI6Im50M2J6NDljNzl2czEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.mYINCj1dKpRVjq52YsZlqlOuEXWccyKLu9plKgn6zP8/image;s=1080x720",
       description: "Witam. Posiadam na sprzedaż auto Mercedes-Benz. W176 wyprodukowany w 2012 roku. Samochód jest bezwypadkowy.", mileage: 65000 },
    { id: 6, brand: "Opel", model: "Crossland X", engine: "1.2", power: 81, production: "2019-06-22T00:00:00", price: 68800, imagePath: "https://ireland.apollo.olxcdn.com/v1/files/eyJmbiI6Im43bGVsMmt4bjU3ODMtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.woCsqbb1YW_YsZIdVZwRQVzKr0FX1Zboww4csbUHa4k/image;s=1080x720",
       description: "Polecam Łukasz Sosiński 601-340-336", mileage: 10 },
    { id: 9, brand: "Mercedes-Benz", model: "Klasa S 350", engine: "3.0", power: 258, production: "2014-01-01T00:00:00", price: 1, imagePath: "https://ireland.apollo.olxcdn.com/v1/files/eyJmbiI6ImNlbjVqamYwdzd1bDEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.lWnYUTdIqEy3BdQoYRWshb5K5V7a3-aBMWP3eGuvWyg/image;s=1080x720",
       description: "Auto bezwypadkowe,\nwręcz pedantycznie utrzymany ,\nZero rys, czy otarć z zewnątrz jak i wewnątrz pojazdu", mileage: 133000 }
  ];
}
