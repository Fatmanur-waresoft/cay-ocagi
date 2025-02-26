namespace deneme.Model
{
    // Beverage sınıfı, içecekleri (Beverages) temsil eden bir modeldir.
    public class Beverage
    {
        // İçeceğin benzersiz kimlik numarası (Primary Key)
        public int id { get; set; }

        // İçeceğin adı
        public string name { get; set; }

        // İçeceğin fiyatı
        public string price { get; set; }

        // İçeceğin fotoğraf veya resim yolu
        public string pics { get; set; }
    }
}
