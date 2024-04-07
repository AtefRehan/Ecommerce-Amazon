import { Product } from "./product";

export interface IPaymentDetails {
    name: string;
    email: string;
    amount: number;
    line1: string;
    postal_code: string;
    city: string;
    state: string;
    countryCode: string;
    products:Product[]
  }
  