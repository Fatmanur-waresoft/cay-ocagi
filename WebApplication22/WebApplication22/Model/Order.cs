namespace deneme.Model
{
    // Order sınıfı, siparişleri (Orders) temsil eden bir modeldir.
    public class Order
    {
        // Siparişin benzersiz kimlik numarası (Primary Key)
        public int id { get; set; }

        // Siparişe ait notlar (Opsiyonel alan)
        public string? notes { get; set; }

        // Siparişin ait olduğu odanın adı
        public string roomname { get; set; }

        // Siparişin durumu (örneğin: "Hazırlanıyor", "Teslim Edildi" gibi)
        public string situation { get; set; }

        // Siparişin toplam fiyatı
        public string price { get; set; }
    }
}
