# Thong tin 
Cach de scafford DbContext tu MySQL
```bash
Scaffold-DbContext 'Name=ConnectionStrings:MySQLConnection' Pomelo.EntityFrameworkCore.MySql -ContextDir Data -OutputDir Models -force
```

Cach tao RSA key cho Asymmetric encryption
```bash
# Tạo private key (bảo mật)
openssl genrsa -out private.key 2048

# Tạo public key từ private key
openssl rsa -in private.key -pubout -out public.key
```


```
SEARCH ALL PRODUCTS:
ko authorize, ten sp, loai sp, minPrice, maxPrice, branch, 
keyboard: switch  
laptop: size
```