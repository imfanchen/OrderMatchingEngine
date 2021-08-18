using System;
using System.Collections.Generic;
using System.Linq;

namespace AkunaHackerRank {

    public class MatchingEngine {
        public const string ORDERSIDE_BUY = "BUY";
        public const string ORDERSIDE_SELL = "SELL";
        public const string ORDERTYPE_GOODFORDAY = "GFD";
        public const string ORDERTYPE_INSERTORCANCEL = "IOC";
        public const string OPERATION_CANCEL = "CANCEL";
        public const string OPERATION_MODIFY = "MODIFY";
        public const string OPERATION_PRINT = "PRINT";
        public HashSet<(string, DateTime)> cancellations = new HashSet<(string, DateTime)>();
        public Dictionary<string, Order> orders = new Dictionary<string, Order>();
        public PriorityQueue<BuyOrder> bids = new PriorityQueue<BuyOrder>(); // maxHeap
        public PriorityQueue<SellOrder> asks = new PriorityQueue<SellOrder>(); // minHeap

        public void Add(string orderId, string orderSide, string orderType, int price, int quantity) {
            Order order = new Order(orderId, orderSide, orderType, price, quantity);
            Match(order);
        }

        public void Match(Order order) {
            if (order.OrderSide == ORDERSIDE_BUY) {
                while (asks.Any() && cancellations.Contains((asks.Peek().OrderId, asks.Peek().Timestamp))) {
                    asks.Dequeue(); // remove previously canceled order from the minHeap
                }
                while (asks.Any() && order.Quantity > 0) {
                    SellOrder selling = asks.Peek(); // min sell offer
                    if (order.Price >= selling.Price) {
                        int tradedQuantity = Math.Min(order.Quantity, selling.Quantity);
                        Console.WriteLine($"TRADE {selling.OrderId} {selling.Price} {tradedQuantity} {order.OrderId} {order.Price} {tradedQuantity}");
                        if (order.Quantity >= selling.Quantity) {
                            asks.Dequeue();
                            orders.Remove(selling.OrderId);
                        } else {
                            selling.Quantity -= tradedQuantity;
                            orders[selling.OrderId].Quantity -= tradedQuantity;
                        }
                        order.Quantity -= tradedQuantity;
                    }
                }
            }
            if (order.OrderSide == ORDERSIDE_SELL) {
                while (bids.Any() && cancellations.Contains((bids.Peek().OrderId, bids.Peek().Timestamp))) {
                    bids.Dequeue(); // remove previously canceled order from the maxHeap
                }
                while (bids.Any() && order.Quantity > 0) {
                    BuyOrder buying = bids.Peek(); // max buy offer
                    if (order.Price <= buying.Price) {
                        int tradedQuantity = Math.Min(order.Quantity, buying.Quantity);
                        Console.WriteLine($"TRADE {buying.OrderId} {buying.Price} {tradedQuantity} {order.OrderId} {order.Price} {tradedQuantity}");
                        if (order.Quantity >= buying.Quantity) {
                            bids.Dequeue();
                            orders.Remove(buying.OrderId);
                        } else {
                            buying.Quantity -= tradedQuantity;
                            orders[buying.OrderId].Quantity -= tradedQuantity;
                        }
                        order.Quantity -= tradedQuantity;
                    }
                }
            }
            if (order.Quantity > 0 && order.OrderType == ORDERTYPE_GOODFORDAY) {
                orders.Add(order.OrderId, order); // for fast retrival while modifying
                if (order.OrderSide == ORDERSIDE_BUY) {
                    bids.Enqueue(new BuyOrder(order)); // for fast retrival while finding matching buy order
                } else {
                    asks.Enqueue(new SellOrder(order)); //for fast retrical while finding matching sell order
                }
            }
        }

        public void Modify(string orderId, string orderSide, int newPrice, int newQuantity) {
            if (!orders.ContainsKey(orderId)) return;
            var order = orders[orderId];
            if (order.OrderType == ORDERTYPE_INSERTORCANCEL) return;
            if (order.OrderSide != orderSide || order.Price != newPrice || order.Quantity != newQuantity) {
                Cancel(orderId);
                Add(orderId, orderSide, ORDERTYPE_GOODFORDAY, newPrice, newQuantity);
            }
        }

        public void Cancel(string orderId) {
            if (!orders.ContainsKey(orderId)) return;
            var order = orders[orderId];
            cancellations.Add((orderId, order.Timestamp));
            orders.Remove(orderId);
        }

        /// <summary>
        /// Print SELL and BUY in decending order based on price.
        /// The quantity represents the sum of all order quantities at the printed price.
        /// </summary>
        public void Print() {
            var asks = orders.Values.Where(x => x.OrderSide == ORDERSIDE_SELL)
                .GroupBy(x => x.Price)
                .OrderByDescending(g => g.Key)
                .Select(g => new { Price = g.Key, Quantity = g.Sum(x => x.Quantity) });

            var bids = from x in orders.Values
                       where x.OrderSide == ORDERSIDE_BUY
                       group x by x.Price into g
                       orderby g.Key descending
                       select new { Price = g.Key, Quantity = g.Sum(x => x.Quantity) };

            Console.WriteLine("SELL:");
            foreach (var ask in asks) {
                Console.WriteLine($"{ask.Price} {ask.Quantity}");
            }

            Console.WriteLine("BUY:");
            foreach (var bid in bids) {
                Console.WriteLine($"{bid.Price} {bid.Quantity}");
            }
        }
    }
}
