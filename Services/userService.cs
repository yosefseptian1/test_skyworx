using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public interface IUserService{
    Task<List<user>> GetAllUser();
    Task<(bool, string)>BuatUser(buatUser param);
    Task<(bool, string)>UpdateUser(updateUser param);
    Task<(bool, string)>DeleteUser(short userid);
    Task<(bool, string)>register(registerUser param);
    Task<(bool, string, string)>login(registerUser param);
}

public class userService : IUserService
{
    private readonly IDbService _db;
    private readonly IConfiguration _configuration;
    public userService(IDbService db, IConfiguration configuration){
        _db = db;
        _configuration = configuration;
    }
    public async Task<List<user>> GetAllUser(){
        try
        {
            var result = await _db.GetAll<user>("select * from select_users()", new {});
            return result;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<(bool, string)> BuatUser(buatUser param){
        try
        {
            var result = await _db.GetAsync<int>("select * from insert_users(@_nama,@_nip,@_branch,@_nohp,@_role)", new {
                _nama = param.nama,
                _nip = param.nip,
                _branch = param.branch,
                _nohp = param.nohp,
                _role = param.role
            });
            if(result <= 0){
                return (false, "gagal insert");
            }
            return (true, "berhasil insert");
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<(bool, string)>UpdateUser(updateUser param){
        try
        {
            var result = await _db.GetAsync<int>("select * from update_user(@_userid,@_nama,@_nip,@_branch,@_nohp,@_role)", new {
                _userid = param.userid,
                _nama = param.nama,
                _nip = param.nip,
                _branch = param.branch,
                _nohp = param.nohp,
                _role = param.role
            });
            if(result <= 0){
                return (false, "gagal update");
            }
            return (true, "berhasil update");
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<(bool, string)>DeleteUser(short userid){
        try
        {
            var result = await _db.GetAsync<bool>("select * from delete_user(@_userid)", new {
                _userid = userid
            });
            if(!result){
                return (false, "gagal delete");
            }
            return (true, "berhasil Delete");
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<(bool, string)>register(registerUser param){
        try
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(param.password);
            
            var result = await _db.GetAsync<bool>("select * from register_user(@_username,@_password)", new {
                _username = param.username,
                _password = passwordHash
            });
            if(!result){
                return (false, "gagal register");
            }
            return (true, "berhasil register");
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<(bool, string, string)>login(registerUser param){
        try
        {
            var result = await _db.GetAsync<getUsername>("select * from select_akun(@_username)", new {
                _username = param.username
            });
            if(result is null){
                return (false, "akun tidak ditemukan", null);
            }
            var passwordHash = BCrypt.Net.BCrypt.Verify(param.password, result.password);
            if(!passwordHash){
                return (false, "password salah", null);
            }

            var token = CreateToken(param);
            return (true, "berhasil login", token);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private string CreateToken(registerUser param){
        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, param.username),
            new Claim(ClaimTypes.Name, param.password)
        };

        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
        if(appSettingsToken is null){
            return "Token belum diisi";
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(10),
            SigningCredentials = creds
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}