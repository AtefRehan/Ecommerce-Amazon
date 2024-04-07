import { Product } from "./product";

export interface IPurchasedItems {
    _id: string;
    name: string;
    email: string;
    phone:string;
    address: {
      line1: string;
      postal_code: string;
      city: string;
      state: string;
      country: string;
      _id: string;
    };
    product: Product[];
    __v: number;
  }

