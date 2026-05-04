using System;
using System.Collections.Generic;

class Produk
{
    public string Nama { get; set; }
    public double Harga { get; set; }

    public Produk(string nama, double harga)
    {
        Nama = nama;
        Harga = harga;
    }

    public virtual void InfoProduk()
    {
        Console.WriteLine($"  Nama     : {Nama}");
        Console.WriteLine($"  Harga    : Rp {Harga:N0}");
        Console.WriteLine($"  Kategori : {Kategori()}");
    }

    public virtual string Kategori() => "Produk Umum";
}

class Elektronik : Produk
{
    public int Garansi { get; set; }

    public Elektronik(string nama, double harga, int garansi) : base(nama, harga)
    {
        Garansi = garansi;
    }

    public void CekGaransi() => Console.WriteLine($"{Nama} - Garansi: {Garansi} bulan");
    public override string Kategori() => "Elektronik";
    public override void InfoProduk()
    {
        base.InfoProduk();
        Console.WriteLine($"  Garansi  : {Garansi} bulan");
    }
}

class Makanan : Produk
{
    public DateTime TanggalKadaluarsa { get; set; }

    public Makanan(string nama, double harga, DateTime kadaluarsa) : base(nama, harga)
    {
        TanggalKadaluarsa = kadaluarsa;
    }

    public void CekKadaluarsa()
    {
        string status = DateTime.Now > TanggalKadaluarsa ? "KADALUARSA" : "Masih layak";
        Console.WriteLine($"{Nama} - {status} (Exp: {TanggalKadaluarsa:dd-MM-yyyy})");
    }

    public override string Kategori() => "Makanan";
}

class Laptop : Elektronik
{
    public Laptop(string nama, double harga, int garansi) : base(nama, harga, garansi) { }
    public void InstallSoftware() => Console.WriteLine($"{Nama}: Software berhasil diinstall!");
    public override string Kategori() => "Laptop";
}

class HP : Elektronik
{
    public HP(string nama, double harga, int garansi) : base(nama, harga, garansi) { }
    public void Telepon() => Console.WriteLine($"{Nama}: Sedang menelepon...");
    public override string Kategori() => "HP";
}

class Snack : Makanan
{
    public Snack(string nama, double harga, DateTime kadaluarsa) : base(nama, harga, kadaluarsa) { }
    public void Makan() => Console.WriteLine($"{Nama}: Sedang dimakan, enak!");
    public override string Kategori() => "Snack";
}

class Minuman : Makanan
{
    public Minuman(string nama, double harga, DateTime kadaluarsa) : base(nama, harga, kadaluarsa) { }
    public void Dinginkan() => Console.WriteLine($"{Nama}: Sedang didinginkan di kulkas!");
    public override string Kategori() => "Minuman";
}

class Toko
{
    private List<Produk> daftarProduk = new List<Produk>();

    public void TambahProduk(Produk produk) => daftarProduk.Add(produk);

    public void DaftarProduk()
    {
        Console.WriteLine("\n=== Daftar Produk Toko ===");
        foreach (var p in daftarProduk)
        {
            Console.WriteLine("---------------------------");
            p.InfoProduk();
        }
        Console.WriteLine("---------------------------");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Toko toko = new Toko();

        Laptop laptop = new Laptop("ASUS VivoBook", 8500000, 24);
        HP hp = new HP("Samsung Galaxy A55", 4999000, 12);
        Snack snack = new Snack("Chitato", 15000, new DateTime(2026, 12, 31));
        Minuman minuman = new Minuman("Aqua", 8000, new DateTime(2027, 6, 30));

        toko.TambahProduk(laptop);
        toko.TambahProduk(hp);
        toko.TambahProduk(snack);
        toko.TambahProduk(minuman);

        toko.DaftarProduk();

        Console.WriteLine("\n=== Polymorphism ===");
        List<Produk> produkList = new List<Produk> { laptop, hp, snack, minuman };
        foreach (Produk p in produkList)
            Console.WriteLine($"{p.Nama} => {p.Kategori()}");

        Console.WriteLine("\n=== Method Khusus ===");
        laptop.InstallSoftware();  
        hp.Telepon();
        snack.Makan();
        minuman.Dinginkan();      
        laptop.CekGaransi();
        snack.CekKadaluarsa();

        Console.WriteLine("\n=== Soal 1 ===");
        Console.WriteLine($"laptop.Kategori() = {laptop.Kategori()}");
        Console.WriteLine($"snack.Kategori()  = {snack.Kategori()}");

        Console.WriteLine("\n=== Soal 2 ===");
        laptop.InstallSoftware();

        Console.WriteLine("\n=== Soal 3 ===");
        laptop.InfoProduk();

        Console.WriteLine("\n=== Soal 4 ===");
        minuman.Dinginkan();

        Console.WriteLine("\n=== Soal 5 ===");
        Produk produkHP = new HP("iPhone 15", 15000000, 12);
        Console.WriteLine($"produkHP.Kategori() = {produkHP.Kategori()}");
    }
}