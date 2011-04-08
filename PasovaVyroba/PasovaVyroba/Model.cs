using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

// TODO: Co kdyz zacnu delat produkt A, a okamzikem, kdy
// to neni hotove, pridam na vstup dalsi?

// TODO: A co s odstavovanim?

// TODO: Po dodelani prace udelat Dispatch().

namespace PasovaVyroba
{
    public class Action : INotifyPropertyChanged
    {
        public Action()
        {
        }
        public Action(int place, int order, int time)
        {
            Place = place; Order = order; Time = time;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
        }

        private int _place;
        public int Place { get { return _place; } set { _place=value; NotifyPropertyChanged("Place"); NotifyPropertyChanged("FullText"); } }
        private int _order;
        public int Order { get { return _order; } set { _order=value; NotifyPropertyChanged("Order"); NotifyPropertyChanged("FullText"); } }
        private int _time;
        public int Time { get { return _time; } set {_time = value; NotifyPropertyChanged("Time"); NotifyPropertyChanged("FullText"); } }

        public string FullText { get { return Order + ": stanoviště " + Place + ", " + Time + " s"; } }
    }
    public class Product : INotifyPropertyChanged
    {
        private string _code;
        public string Code { get { return _code; } set { _code = value; NotifyPropertyChanged("Code"); NotifyPropertyChanged("FullText"); } }
        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); NotifyPropertyChanged("FullText"); } }
        public string FullText { get { return Code + ": " + Name; } }

        public int FreeNextOrder
        {
            get
            {
                if (actions.Count > 0)
                {
                    int max = actions.Max<Action>(a => a.Order); //(Func<in Action, out double>)
                    return max + 10 - (max % 10);
                }
                return 10;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;   
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
        }

        private BindingList<Action> actions;
        public BindingList<Action> Actions
        {
            get
            {
                return actions;
            }
            set
            {
                actions = value;
            }
        }
        public List<Action> SortedActions
        {
            get
            {
                List<Action> acts = new List<Action>(actions);
                acts.Sort(new ActionComparer());
                return acts;
            }
        }

        public Product()
        {
            actions = new BindingList<Action>();
        }

        public Product(string name, string code):this()
        {
            Code = code; Name = name;
        }

        public override string ToString()
        {
            return Code + ": " + Name;
        }
    }
    class ProductComparer : IComparer<Product>
    {
        public int Compare(Product a, Product b)
        {
            return a.Code.CompareTo(b.Code);
        }
    };
    class ActionComparer : IComparer<Action>
    {
        public int Compare(Action a, Action b)
        {
            return a.Order.CompareTo(b.Order);
        }
    };
    public enum WorkplaceState
    {
        NoJob, Working
    };
    public class ProductInstance
    {
        public Product Product;
        public int Job;

        public ProductInstance(Product product, int job)
        {
            Product = product; Job = job;
        }
        public bool IsFinished { get { return Product.Actions.Count <= Job; } }
    };

    public class Swapspace : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            context.Post(new SendOrPostCallback(delegate(object state)
            {
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
            }), null);
        }
        
        private ProductHeap contents;
        public ProductHeap Contents {
            get { return contents; }
            set {
                contents = value;
                NotifyPropertyChanged("Contents");
            }
        }

        private Point location;
        public Point Location
        {
            get { return location; }
            set { location = value; NotifyPropertyChanged("Location"); }
        }

        SynchronizationContext context;
        public Swapspace(Point location, SynchronizationContext context)
        {
            this.context = context;
            Location = location;
            contents = new ProductHeap(null, 0);
        }

    }

    // TODO: NotifyPropertyChanged
    public class Workplace : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            context.Post(new SendOrPostCallback(delegate(object state)
            {
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
            }), null);
        }
        public int Identification { get; set; }

        private TimeSpan totalTime;
        private DateTime start;

        private WorkplaceState state = WorkplaceState.NoJob;
        public WorkplaceState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public TimeSpan TotalTime
        {
            get
            {
                return totalTime;
            }
            set
            {
                totalTime = value;
            }
        }
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }
        public TimeSpan Elapsed
        {
            get
            {
                return (DateTime.Now - Start);
            }
        }
        public double Progress
        {
            get
            {
                if (TotalTime.TotalMilliseconds == 0 || State != WorkplaceState.Working) return 0;
                return (Elapsed.TotalMilliseconds) / (TotalTime.TotalMilliseconds);
            }
        }
        public Color Color
        {
            get { return Color.Blue; }
        }
        private Point location;
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        private ProductHeap contents;
        public ProductHeap Contents
        {
            get { return contents; }
            set {
                contents = value;
                if (!contents.Empty)
                {
                    Program.Model.StartWorkplaceThread(this);
                    Start = DateTime.Now;
                    TotalTime = TimeSpan.FromSeconds(contents.Count * contents.Product.Product.Actions[contents.Product.Job].Time);
                    State = WorkplaceState.Working;
                }
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("TotalTime");
                NotifyPropertyChanged("Contents");
                FireUpdate();
            }
        }
        SynchronizationContext context;
        public Workplace(int identification, Point location, SynchronizationContext context)
        {
            this.context = context;
            Identification = identification;
            Location = location;
            contents = new ProductHeap(null, 0);
        }

        public EventHandler Updated;
        public EventHandler Finished;

        public void FireUpdate()
        {
            if (Updated != null) Updated(this, EventArgs.Empty);
        }

        public void FireFinished()
        {
            this.State = WorkplaceState.NoJob;
            contents.Product.Job++;
            if (Finished != null) Finished.Invoke(this, EventArgs.Empty);
        }
    };
    public delegate void EndPointClearedHandler();
    public class EndPoint : INotifyPropertyChanged
    {
        private SynchronizationContext context;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            context.Post(new SendOrPostCallback(delegate(object state)
            {
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
            }), null);
        }
        private Point location;
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        private ProductHeap contents;
        public ProductHeap Contents {
            get
            {
                return contents;
            }
            set
            {
                contents = value;
                NotifyPropertyChanged("Contents");
                NotifyPropertyChanged("HasContents");
            }
        }
        public void Clear()
        {
            Contents = new ProductHeap(null, 0);
            if (Cleared != null) Cleared();
        }
        public EndPointClearedHandler Cleared;
        public EndPoint(Point location, SynchronizationContext context) {
            contents = new ProductHeap(null, 0);
            this.location = location;
            this.context = context;
        }
        public bool HasContents
        {
            get { return Contents.Count > 0 && Contents.Product != null; }
        }
    };
    public class ProductHeap : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
        }
        public ProductInstance Product;
        public int Count;

        public ProductHeap(ProductInstance product, int count) { Product = product; Count = count; }
        public override string ToString()
        {
            if (Empty)
            {
                return "(nic)";
            }
            if (Product.Job == Product.Product.Actions.Count - 1) return String.Format("{0}x {1}", Count, Product.Product.Name);
            else return String.Format("{0}x {1} ({2}/{3})", Count, Product.Product.Name, Product.Job, Product.Product.Actions.Count);
        }
        public bool Empty
        {
            get { return Product == null || Count == 0; }
        }
        public bool IsFinished
        {
            get { return Product.IsFinished; }
        }
    };
    public class PathNode
    {
        public Point Location;
        public int Index;
        public List<PathEdge> AdjacentEdges;

        public PathNode(int i, int x, int y)
        {
            Location = new Point(x, y);
            Index = i;
            AdjacentEdges = new List<PathEdge>();
        }
    };   
    public class PathEdge
    {
        public PathNode A, B;

        public PathEdge(PathNode a, PathNode b)
        {
            A = a; B = b;
        }

        public override string ToString()
        {
            return A.Index + " <-> " + B.Index;
        }

        public double Angle
        {
            get
            {
                return Math.Atan2(B.Location.Y - A.Location.Y, B.Location.X - A.Location.X);
            }
        }
    };
    public class Model : INotifyPropertyChanged
    {
        public SynchronizationContext context;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            context.Post(new SendOrPostCallback(delegate(object state)
            {
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(info)); }
            }), null);
        }
        public string Filename { get { return "C:\\bla\\vyrobky.xml"; } }

        private BindingList<Product> products;
        public BindingList<Product> Products { get { return products; } }

        public PathNode[] PathNodes;
        public PathEdge[] PathEdges;

        private List<Workplace> workplaces;
        public List<Workplace> Workplaces
        {
            get
            {
                return workplaces;
            }
        }

        private List<Swapspace> swapspaces;
        public List<Swapspace> Swapspaces
        {
            get
            {
                return swapspaces;
            }
        }

        public int[] WorkplaceIndexes
        {
            get
            {
                int[] indexes = new int[workplaces.Count];
                for (int i = 0; i < workplaces.Count; i++) indexes[i] = workplaces[i].Identification;
                return indexes;
            }
        }

        public List<Product> SortedProducts
        {
            get
            {
                List<Product> prods = new List<Product>(products);
                prods.Sort(new ProductComparer());
                return prods;
            }
        }

        public void EndpointCleared()
        {
            context.Post(new SendOrPostCallback(delegate(object state)
            {
                Dispatch();
            }), null);
        }

        public Model()
        {
            double coordinateMultiplierX = 2, coordinateMultiplierY = 1.7;
            context = SynchronizationContext.Current;
            if (context == null)
            {
                context = new SynchronizationContext();
            }

            CarrierPath = new Queue<PathNode>();
            products = new BindingList<Product>();
            workplaces = new List<Workplace> {
                new Workplace(201, new Point(50,50), context),
                new Workplace(202, new Point(200,50), context),
                new Workplace(203, new Point(350, 50), context),
                new Workplace(204, new Point(50,200), context),
                new Workplace(205, new Point(200,200), context),
                new Workplace(300, new Point(350,200), context),
                new Workplace(401, new Point(50,350), context),
                new Workplace(402, new Point(200, 350), context),
                new Workplace(600, new Point(350,350), context),
                new Workplace(900, new Point(500,350), context) };

            foreach (Workplace w in Workplaces)
            {
                w.Location = new Point((int)(w.Location.X * coordinateMultiplierX), (int)(w.Location.Y * coordinateMultiplierY));
            }

            swapspaces = new List<Swapspace>
            {
                new Swapspace(new Point(200,500), context),
                new Swapspace(new Point(650, 125), context)
            };

            foreach (Swapspace s in Swapspaces)
            {
                s.Location = new Point((int)(s.Location.X * coordinateMultiplierX), (int)(s.Location.Y * coordinateMultiplierY));
            }

            carrierContents = new ProductHeap(null, 0);
            CarrierPosition = new Point((int)(125 * coordinateMultiplierX), (int)(50 * coordinateMultiplierY));
            startContents = new ProductHeap(null, 0);
            endPoints = new EndPoint[2];
            endPoints[0] = new EndPoint(new Point(50, 500), context);
            endPoints[1] = new EndPoint(new Point(350, 500), context);

            foreach (EndPoint e in endPoints)
            {
                e.Location = new Point((int)(e.Location.X * coordinateMultiplierX), (int)(e.Location.Y * coordinateMultiplierY));
                e.Cleared += new EndPointClearedHandler(EndpointCleared);
            }

            StartLocation = new Point((int)(500 * coordinateMultiplierX), (int)(50 * coordinateMultiplierY));

            PathNodes = new PathNode[] {
                new PathNode(0,50,50), new PathNode(1,125, 50), new PathNode(2,200, 50), new PathNode(3,350, 50), new PathNode(4,500, 50),
                new PathNode(5,125, 125), new PathNode(6,200, 125), new PathNode(7,275, 125), new PathNode(8,350, 125), new PathNode(9,500, 125),
                new PathNode(10,50,200), new PathNode(11,125,200), new PathNode(12,200, 200), new PathNode(13,350, 200),
                new PathNode(14,125, 275), new PathNode(15,275, 275),
                new PathNode(16,50,350), new PathNode(17,125, 350), new PathNode(18,200, 350), new PathNode(19,275, 350), new PathNode(20,350, 350), new PathNode(21,500, 350),
                new PathNode(22,50,500), new PathNode(23,125, 500), new PathNode(24,275, 500), new PathNode(25,350, 500),
                new PathNode(26,200,500), new PathNode(27, 650, 125)
            };

            foreach (PathNode n in PathNodes)
            {
                n.Location = new Point((int)(n.Location.X * coordinateMultiplierX), (int)(n.Location.Y * coordinateMultiplierY));
            }

            int[,] tmp = new int[,] {
                {0,1}, {1,5}, {5,6}, {2,6}, {6,7}, {7,8}, {3,8}, {8,9}, {9,4},
                {10,11}, {5,11}, {6, 12}, {7, 15}, {8, 13}, {11, 14},
                {14, 17}, {16, 17}, {17,23}, {14,15}, {19,20}, {19,24},
                {22,23}, {24,25}, {9, 21}, {15, 19}, {18, 19},
                {23,26}, {24, 26}, {9,27}

            };

            PathEdges = new PathEdge[tmp.GetLength(0)];
            for (int i = 0; i < tmp.GetLength(0); i++)
            {
                PathEdges[i] = new PathEdge(PathNodes[tmp[i, 0]], PathNodes[tmp[i, 1]]);
            }

            foreach (PathEdge e in PathEdges)
            {
                e.A.AdjacentEdges.Add(e);
                e.B.AdjacentEdges.Add(e);
            }

            foreach (Workplace w in Workplaces)
            {
                w.Finished += new EventHandler((a, b) => { Dispatch(); });
            }
        }
        public void Save()
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("produkty");
            xml.AppendChild(root);
            foreach (Product product in SortedProducts)
            {
                XmlElement produkt = xml.CreateElement("produkt");
                root.AppendChild(produkt);
                produkt.Attributes.Append(xml.CreateAttribute("kod"));
                produkt.Attributes.Append(xml.CreateAttribute("jmeno"));
                produkt.Attributes["kod"].Value = product.Code;
                produkt.Attributes["jmeno"].Value = product.Name;

                foreach (Action action in product.SortedActions)
                {
                    XmlElement akce = xml.CreateElement("akce");
                    produkt.AppendChild(akce);
                    akce.Attributes.Append(xml.CreateAttribute("poradi"));
                    akce.Attributes.Append(xml.CreateAttribute("stanoviste"));
                    akce.Attributes.Append(xml.CreateAttribute("cas"));
                    akce.Attributes["poradi"].Value = action.Order.ToString();
                    akce.Attributes["stanoviste"].Value = action.Place.ToString();
                    akce.Attributes["cas"].Value = action.Time.ToString();
                }
            }
            xml.Save(Filename);
        }
        public void Load()
        {
            if (!File.Exists(Filename)) return;
            products.Clear();
            XmlDocument xml = new XmlDocument();
            xml.Load(Filename);
            XmlElement root = xml.DocumentElement;
            foreach (XmlNode produkt in root.ChildNodes)
            {
                Product product = new Product();
                products.Add(product);
                product.Code = produkt.Attributes["kod"].Value;
                product.Name = produkt.Attributes["jmeno"].Value;

                foreach (XmlNode akce in produkt.ChildNodes)
                {
                    Action action = new Action();
                    product.Actions.Add(action);
                    action.Order = int.Parse(akce.Attributes["poradi"].Value);
                    action.Place = int.Parse(akce.Attributes["stanoviste"].Value);
                    action.Time = int.Parse(akce.Attributes["cas"].Value);
                }
            }
        }

        private Dictionary<Workplace, WorkplaceThread> workplaceThreads;

        public void StartWorkplaceThread(Workplace workplace)
        {
            if (workplaceThreads == null) workplaceThreads = new Dictionary<Workplace, WorkplaceThread>();
            if (!workplaceThreads.ContainsKey(workplace)) workplaceThreads.Add(workplace, new WorkplaceThread(workplace));

            workplaceThreads[workplace].Run();
        }

        public void StopAllThreads()
        {
            if (workplaceThreads == null) return;
            foreach (WorkplaceThread wpt in workplaceThreads.Values)
            {
                wpt.Stop();
            }
        }

        public EventHandler CarrierPositionChanged;
        private ProductHeap carrierContents;
        private PointF carrierPosition;
        public PointF CarrierPosition
        {
            get { return carrierPosition; }
            set { carrierPosition = value; }
        }
        public Point CarrierPositionP
        {
            get
            {
                return new Point((int)carrierPosition.X,
                    (int)carrierPosition.Y);
            }
        }
        public ProductHeap CarrierContents
        {
            get { return carrierContents; }
            private set
            {
                carrierContents = value;
                NotifyPropertyChanged("CarrierContents");
            }
        }

        public int EndCount { get { return 2; } }
        private ProductHeap startContents;
        private EndPoint[] endPoints;

        public Point StartLocation;
        public EndPoint[] EndPoints
        {
            get { return endPoints; }
        }
        public ProductHeap StartContents
        {
            get { return startContents; }
            private set { startContents = value; NotifyPropertyChanged("StartContents"); NotifyPropertyChanged("StartIsEmpty"); }
        }

        public void FireCarrierPositionChanged()
        {
            if (CarrierPositionChanged != null) CarrierPositionChanged(this, EventArgs.Empty);
        }

        public bool StartIsEmpty
        {
            get { return StartContents.Empty; }
        }

        public void AddToStart(Product product, int count)
        {
            StartContents = new ProductHeap(new ProductInstance(product, 0), count);
            if (NoOrders) Dispatch();
        }

        public Queue<PathNode> CarrierPath;

        public EndPoint FindEndPointAtPosition(Point position)
        {
            foreach (EndPoint ep in EndPoints)
            {
                if (ep.Location.Equals(position)) return ep;
            }
            return null;
        }

        public Swapspace FindSwapspaceAtPosition(Point position)
        {
            foreach (Swapspace ep in Swapspaces)
            {
                if (ep.Location.Equals(position)) return ep;
            }
            return null;
        }

        private int FindPathNodeID(Point position)
        {
            for (int i = 0; i < PathNodes.Length; i++)
                if (PathNodes[i].Location.Equals(position)) return i;
            return int.MaxValue;
        }

        private bool IsImportantPoint(PathNode node)
        {
            Point position = node.Location;
            foreach (Workplace w in Workplaces) if (w.Location.Equals(position)) return true;
            foreach (EndPoint e in EndPoints) if (e.Location.Equals(position)) return true;
            if (position.Equals(StartLocation)) return true;
            return false;
        }

        private PathNode FindNearestUnimportant(Point position)
        {
            // TODO: Dostan se tam skrz Queue.

            // Nejdriv najdi ten PathNode, na ktery chci.

            int target = FindPathNodeID(position);
            if (target == int.MaxValue) throw new ArgumentException("That's not a node!");
            int[] distances = new int[PathNodes.Length];
            for (int i = 0; i < PathNodes.Length; i++) distances[i] = int.MaxValue;
            distances[target] = 0;

            Queue<int> projit = new Queue<int>();
            projit.Enqueue(target);

            while (projit.Count > 0)
            {
                PathNode node = PathNodes[projit.Dequeue()];
                if (!IsImportantPoint(node))
                {
                    return node;
                }
                foreach (PathEdge e in node.AdjacentEdges)
                {
                    if ((e.A.Index == node.Index) && (distances[e.B.Index] > distances[node.Index] + 1))
                    {
                        distances[e.B.Index] = distances[node.Index] + 1;
                        projit.Enqueue(e.B.Index);
                    }
                    if ((e.B.Index == node.Index) && (distances[e.A.Index] > distances[node.Index] + 1))
                    {
                        distances[e.A.Index] = distances[node.Index] + 1;
                        projit.Enqueue(e.A.Index);
                    }
                }
            }
            return null;
        }

        private void PathFind(Point position)
        {
            // TODO: Dostan se tam skrz Queue.
            
            // Nejdriv najdi ten PathNode, na ktery chci.

            int target, carrier;
            if (FindPathNodeID(CarrierPositionP) == int.MaxValue && CarrierPath.Count > 0)
            {
                carrier = CarrierPath.ToArray()[0].Index;
            }
            else
            {
                carrier = FindPathNodeID(CarrierPositionP);
            }
            target = FindPathNodeID(position);
            if (target == int.MaxValue) throw new ArgumentException("That's not a node!");
            if (carrier == int.MaxValue) throw new ArgumentException("Carrier not currently at a node!");
            int[] distances = new int[PathNodes.Length];
            for (int i = 0; i < PathNodes.Length; i++) distances[i] = int.MaxValue;
            distances[target] = 0;

            Queue<int> projit = new Queue<int>();
            projit.Enqueue(target);

            while (projit.Count > 0 && distances[carrier]==int.MaxValue)
            {
                PathNode node = PathNodes[projit.Dequeue()];
                foreach (PathEdge e in node.AdjacentEdges)
                {
                    if ((e.A.Index == node.Index) && (distances[e.B.Index] > distances[node.Index] + 1))
                    {
                        distances[e.B.Index] = distances[node.Index] + 1;
                        projit.Enqueue(e.B.Index);
                    }
                    if ((e.B.Index == node.Index) && (distances[e.A.Index] > distances[node.Index] + 1))
                    {
                        distances[e.A.Index] = distances[node.Index] + 1;
                        projit.Enqueue(e.A.Index);
                    }
                }
            }

            CarrierPath.Clear();
            if (!PathNodes[carrier].Equals(CarrierPositionP)) CarrierPath.Enqueue(PathNodes[carrier]);

            // vytracuj to zpet...
            PathNode current = PathNodes[carrier];
            do
            {
                foreach (PathEdge e in current.AdjacentEdges)
                {
                    if (e.A.Index == current.Index && distances[e.B.Index] < distances[current.Index])
                    {
                        current = e.B;
                        CarrierPath.Enqueue(current);
                    }
                    if (e.B.Index == current.Index && distances[e.A.Index] < distances[current.Index])
                    {
                        current = e.A;
                        CarrierPath.Enqueue(current);
                    }
                }
            } while (current.Index != PathNodes[target].Index);
        }

        public float Distance(PointF A, PointF B)
        {
            return (float)Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
        }

        private System.Threading.Timer PositionUpdateTimer;

        public void StartPositionUpdater()
        {
            PositionUpdateTimer = new System.Threading.Timer(new TimerCallback(UpdatePosition));
            PositionUpdateTimer.Change(20, 20);
        }

        public void UpdatePosition(object sender)
        {
            lock (PositionUpdateTimer)
            {
                if (CarrierPath.Count < 1) return; // nic.

                // rekneme ze konstantne 2 pixely (coz je fuj)
                float dist = 2;

                Point cTP = CarrierPath.ToArray()[0].Location;
                PointF currentTarget = new PointF(cTP.X, cTP.Y);

                float vzdalenost = Distance(currentTarget, carrierPosition);
                if (vzdalenost < dist)
                {
                    carrierPosition = currentTarget;
                    CarrierPath.Dequeue();
                    if (CarrierPath.Count == 0) Dispatch();
                }
                else
                {
                    PointF v = new PointF(
                        (currentTarget.X - carrierPosition.X) / vzdalenost,
                        (currentTarget.Y - carrierPosition.Y) / vzdalenost);
                    // TODO: Pozici voziku PointF
                    carrierPosition = new PointF(
                        carrierPosition.X + v.X * dist,
                        carrierPosition.Y + v.Y * dist);
                    FireCarrierPositionChanged();
                }
            }
        }

        public bool NoOrders { get { return CarrierPath.Count == 0; } }

        public bool DispatchLater = false;

        public bool CarrierAtNode()
        {
            foreach (PathNode n in PathNodes)
            {
                if (n.Location.Equals(carrierPosition)) return true;
            }
            return false;
        }

        protected bool FreeEndpointExists()
        {
            foreach (EndPoint ep in EndPoints)
            {
                if (ep.Contents.Empty) return true;
            }
            return false;
        }

        protected bool FreeSwapspaceExists()
        {
            foreach (Swapspace ep in Swapspaces)
            {
                if (ep.Contents.Empty) return true;
            }
            return false;
        }

        protected bool CanDirectlyDealWith(ProductHeap heap)
        {
            return (heap.IsFinished && FreeEndpointExists()) || ((!heap.IsFinished) && (Workplaces[heap.Product.Product.Actions[heap.Product.Job].Place].Contents.Empty));
        }

        protected bool CanDealWith(ProductHeap heap)
        {
            return (heap.IsFinished && (FreeEndpointExists() || FreeSwapspaceExists())) || ((!heap.IsFinished) && (Workplaces[heap.Product.Product.Actions[heap.Product.Job].Place].Contents.Empty || FreeSwapspaceExists()));
        }

        // TRUE: queue je neprazdna.
        // FALSE: queue je prazdna.
        public bool Dispatch()
        {
            Swapspace sws = FindSwapspaceAtPosition(CarrierPositionP);
            // 1) Je neco ve voziku?
            if (!CarrierContents.Empty)
            {
                // 1A) Je produkt ve voziku hotovy?
                if (CarrierContents.Product.IsFinished)
                {
                    EndPoint ep = FindEndPointAtPosition(CarrierPositionP);
                    // Vyloz ho nebo odvez na nejblizsi odkladiste.
                    if (ep == null || !ep.Contents.Empty)
                    {
                        // TODO: Do nejblizsiho prazdneho odkladiste, ne prvniho v poradi.
                        foreach (EndPoint e in EndPoints)
                        {
                            if (e.Contents.Empty)
                            {
                                PathFind(e.Location);
                                Log("Vezu to ke konci.");
                                return true;
                            }
                        }
                        Log("Vsechny konce jsou plne. Budu delat neco jineho.");
                        //return false;
                    }
                    else
                    {
                        // Vyloz.
                        ep.Contents = CarrierContents;
                        CarrierContents = new ProductHeap(null, 0);
                        Log("Vykladam na konci.");
                        return Dispatch();
                    }
                }
                else
                {
                    Workplace wp = Workplaces[CarrierContents.Product.Product.Actions[CarrierContents.Product.Job].Place];
                    // Odvez produkt ve voziku na dalsi stanoviste.
                    if (wp.Contents.Empty)
                    {
                        if (CarrierPositionP.Equals(wp.Location))
                        {
                            // Vyloz.
                            wp.Contents = CarrierContents;
                            CarrierContents = new ProductHeap(null, 0);
                            Log(String.Format("Vykladam na dalsim stanovisti ({0}).", wp.Identification));
                            return Dispatch();
                        }
                        else
                        {
                            PathFind(wp.Location);
                            Log(String.Format("Jdu na dalsi stanoviste ({0}).", wp.Identification));
                            return true;
                        }
                    }
                    else
                    {
                        // Protoze je dalsi plny, musim to zavest na odkladiste

                        // TODO: Nejblizsi.
                        Swapspace swapspace = FindSwapspaceAtPosition(CarrierPositionP);
                        if (swapspace == null)
                        {
                            foreach (Swapspace s in Swapspaces)
                            {
                                if (s.Contents.Empty)
                                {
                                    PathFind(s.Location);
                                    return true;
                                }
                            }
                            Log(String.Format("Castecny deadlock (stanoviste {0} je plne, ale neni swapspace).", wp.Identification));
                        }
                        else
                        {
                            swapspace.Contents = CarrierContents;
                            CarrierContents = new ProductHeap(null, 0);
                            Log("Vykladam na swapspacu.");
                            return Dispatch();
                        }
                    }
                }
            }
            else
            {
                if (sws != null && !sws.Contents.Empty && CanDirectlyDealWith(sws.Contents))
                {
                    CarrierContents = sws.Contents;
                    sws.Contents = new ProductHeap(null, 0);
                    Log("Vzal jsem si neco ze swapspace.");
                    return Dispatch();
                }
                // 2) Je neco na startu?
                if (!StartIsEmpty)
                {
                    // Jdi na start a seber to.
                    if (CanDealWith(StartContents))
                    {
                        if (CarrierPositionP.Equals(StartLocation))
                        {
                            CarrierContents = StartContents;
                            StartContents = new ProductHeap(null, 0);
                            Log("Sbiram polotovar ze startu.");
                            return Dispatch();
                        }
                        else
                        {
                            PathFind(StartLocation);
                            Log("Jdu na start sebrat polotovar.");
                            return true;
                        }
                    }
                }
                
                {
                    List<Workplace> tenders = new List<Workplace>();
                    foreach (Workplace wp in Workplaces)
                    {
                        if (!wp.Contents.Empty && wp.State == WorkplaceState.NoJob && CanDealWith(wp.Contents))
                        {
                            // Naviguj tam.
                            if (wp.Location.Equals(CarrierPositionP))
                            {
                                // Seber to.
                                CarrierContents = wp.Contents;
                                wp.Contents = new ProductHeap(null, 0);
                                Log("Sbiram produkt ze stanoviste.");
                                return Dispatch();
                            }
                            tenders.Add(wp);
                        }
                    }
                    // TODO: nejblizsi, ne prvni.
                    if (tenders.Count > 0)
                    {
                        PathFind(tenders[0].Location);
                        Log("Jedu si na stanoviste pro produkt.");
                        return true;
                    }
                    else Log("Jel bych pro produkt, ale nemel bych kam s nim.");
                }
            }
            
            // Neni "normalni prace". Vezmi prvni vec z odkladist.
            if (CarrierContents.Empty && (sws == null || sws.Contents.Empty))
            {
                foreach (Swapspace sw in Swapspaces)
                {
                    if (!sw.Contents.Empty && CanDealWith(sw.Contents))
                    {
                        PathFind(sw.Location);
                        Log("Jedu si do swapspace pro produkt. Dalsi misto je prazdne, nevznikne deadlock.");
                        return true;
                    }
                }
            }

            // TODO: Neni vubec prace, vypadni od vsech klicovych bodu.
            if (FindPathNodeID(CarrierPositionP) != int.MaxValue && IsImportantPoint(PathNodes[FindPathNodeID(CarrierPositionP)]))
            {
                Log("Neni prace. Odstupuji od dulezitych bodu.");
                PathNode nearestUnimportant = FindNearestUnimportant(CarrierPositionP);
                if (nearestUnimportant != null)
                {
                    PathFind(nearestUnimportant.Location);
                    return true;
                }
            }

            Log("Není žádná vykonatelná práce.");
            return false;
        }

        public void Log(string s)
        {
            if (LogHandler != null) LogHandler(s);
            Trace.WriteLine(s);
        }

        public event LogHandler LogHandler;
    }

    public delegate void LogHandler(string text);
}
