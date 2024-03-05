public class user{
    public short userid {get;set;}
    public string nama {get;set;}
    public string nip {get;set;}
    public string branch {get;set;}
    public string nohp {get;set;}
    public string role {get;set;}
    public string branch_kode {get;set;}
    public int branch_id {get;set;}
    public string branch_nama {get;set;}
    public string branch_address {get;set;}
    public string branch_tlpn {get;set;}
    public string branch_city {get;set;}
}

public class buatUser{
    public string nama {get;set;}
    public string nip {get;set;}
    public string branch {get;set;}
    public string nohp {get;set;}
    public string role {get;set;}
}

public class registerUser{
    public string username {get;set;}
    public string password {get;set;}
}

public class  getUsername{
    public string username {get;set;}
    public string password {get;set;}
}

public class updateUser{
    public short userid {get;set;}
    public string nama {get;set;}
    public string nip {get;set;}
    public string branch {get;set;}
    public string nohp {get;set;}
    public string role {get;set;}
}