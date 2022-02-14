using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMatchingEngine {

    public class Order {
        public string OrderId { get; set; }
        public string OrderSide { get; set; }
        public string OrderType { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }

        public Order() {
            // default constructor for inheritance
        }

        public Order(string orderId, string orderSide, string orderType, int price, int quantity) {
            OrderId = orderId;
            OrderSide = orderSide;
            OrderType = orderType;
            Price = price;
            Quantity = quantity;
            Timestamp = DateTime.Now;
        }
    }

    public class BuyOrder : Order, IComparable<BuyOrder> {

        public BuyOrder(Order order) {
            OrderId = order.OrderId;
            OrderSide = order.OrderSide;
            OrderType = order.OrderType;
            Price = order.Price;
            Quantity = order.Quantity;
            Timestamp = order.Timestamp;
        }

        public int CompareTo(BuyOrder other) {
            if (Price != other.Price) {
                // better means higher for BUY orders
                return -Price.CompareTo(other.Price);
            }
            return Timestamp.CompareTo(other.Timestamp);
        }
    }

    public class SellOrder : Order, IComparable<SellOrder> {

        public SellOrder(Order order) {
            OrderId = order.OrderId;
            OrderSide = order.OrderSide;
            OrderType = order.OrderType;
            Price = order.Price;
            Quantity = order.Quantity;
            Timestamp = order.Timestamp;
        }

        public int CompareTo(SellOrder other) {
            if (Price != other.Price) {
                // better means lower for SELL orders
                return Price.CompareTo(other.Price);
            }
            return Timestamp.CompareTo(other.Timestamp);
        }
    }
}
