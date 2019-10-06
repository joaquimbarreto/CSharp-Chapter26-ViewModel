using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Customers
{
    public class ViewModel : INotifyPropertyChanged
    {
        private List<Customer> customers;
        private int currentCustomer;
        public Command NextCustomer { get; private set; }
        public Command PreviousCustomer { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ViewModel()
        {
            this.currentCustomer = 0;
            this.IsAtStart = true;
            this.IsAtEnd = false;
            this.NextCustomer = new Command(this.Next, () => this.customers.Count > 1 && !this.IsAtEnd);
            this.PreviousCustomer = new Command(this.Previous, () => this.customers.Count > 0 && !this.IsAtStart);
            this.customers = new List<Customer>
            {
                new Customer
                {
                    CustomerID = 1,
                    Title = "Mr",
                    FirstName = "John",
                    LastName = "Sharp",
                    EmailAddress = "john@contoso.com",
                    Phone = "111-1111"
                },
                new Customer
                {
                    CustomerID = 2,
                    Title = "Ms",
                    FirstName = "Bunny",
                    LastName = "Rabbit",
                    EmailAddress = "uma@pulp.fiction",
                    Phone = "555-4311"
                },
                new Customer
                {
                    CustomerID = 3,
                    Title = "Mrs",
                    FirstName = "Grace",
                    LastName = "Robertson",
                    EmailAddress = "hello@world.com",
                    Phone = "555-1651"
                }
            };
        }

        private bool _isAtStart;
        public bool IsAtStart
        {
            get => this._isAtStart;
            set
            {
                this._isAtStart = value;
                this.OnPropertyChanged(nameof(IsAtStart));
            }
        }

        private bool _isAtEnd;
        public bool IsAtEnd
        {
            get => this._isAtEnd;
            set
            {
                this._isAtEnd = value;
                this.OnPropertyChanged(nameof(IsAtEnd));
            }
        }

        public Customer Current
        {
            get => this.customers.Count > 0 ? this.customers[currentCustomer] : null;
        }

        private void Next()
        {
            if (this.customers.Count - 1 > this.currentCustomer)
            {
                this.currentCustomer++;
                this.OnPropertyChanged(nameof(Current));
                this.IsAtStart = false;
                this.IsAtEnd = (this.customers.Count - 1 == this.currentCustomer);
            }
        }

        private void Previous()
        {
            if (this.customers.Count > 0)
            {
                this.currentCustomer--;
                this.OnPropertyChanged(nameof(Current));
                this.IsAtEnd = false;
                this.IsAtStart = (this.currentCustomer == 0);
            }
        }
    }
}
