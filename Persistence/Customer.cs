namespace Persistence
{
    public class Customer
    {
        public int? CustmerId {set;get;}
        public string CustomerName {set;get;}
        public string CustomerAddress{set;get;}

        public override bool Equals(object obj){
            if(obj is Customer){
                return ((Customer)obj).CustmerId.Equals(CustmerId);
            }
            return false;
        }

        public override int GetHashCode(){
            return CustmerId.GetHashCode();
        }
    }
}
