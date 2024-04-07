export interface OrderData {
  orderId: number;
  createdAt: string;
  shippingDate: string;
  total: number;
  isCancelled: boolean;
  applicationUserId: string;
  cardType: string;
  orderProducts: OrderProduct[];
}

export interface OrderProduct {
  productId: number;
  image: string;
  name: string;
  price: number;
}
