# Sample.WebApi

### Authorization

###### JWT官网, 用于生成token
https://jwt.io/

Header:
```json
{
  "alg": "RS256",
  "typ": "JWT"
}
```

Payload:
```json
{
  "sub": "1234567890",
  "name": "Cloud Hong",
  "admin": true,
  "iss": "www.google.com",
  "exp": "1511212152"
}
```

Note: 
* iss关键字是必须的, 否则会出问题
* 使用RS256作为算法
* exp关键字为可选, 用于设置过期时间

###### 生成public key和private key
http://travistidwell.com/blog/2013/09/06/an-online-rsa-public-and-private-key-generator/

-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQDj6Uvptmo3u21Us5Z+QclmNSKrQpM2ubMaj6kz/fE+FIiOldey
UzvS5wA9k3MiS+PXE1gsU5xDmVVI7hHYRWdAvZiMTGS3dxXYAYnprTL4/wub6Y/9
6uxrcPoHKxtGIrQt2jqGhFGcitD1nWmdEtPjee/3fhduAuEiCtjOnqit1QIDAQAB
AoGBAJdT4YbWCyLkPQzfjY5ZqhtGLrXeJ5dPp/974hJWi+b3hVB/Z8/M+kzn+r3n
+KuODkNRYdtUzM4JspoRESIzuwD9tqAqhykSTP0gIvLKTYwJVx2+uSaCN+pyldgq
tBoIxfb/t+nOfb7lTQhYP8ZWXWkdBsZcZvpgTOY3/GAGMGeFAkEA+SCDpYwczt9+
LEDXbEWhcpkSVdc6+9Oq7/A7jah5nvVbwh6UyR0T0UepYOxPL2Wo9Wj0LFyJQ4IW
EEdZMZVetwJBAOoy8bgCyCveRgk4aricBlcHUxhid8Or6vuCaqRP9gyvRe7REuE3
1QoEPlC894lW4EN5OIq/EmFsbvqkJKSUS9MCQQCI46fS0GGH/uBKmrqEYOJsoNWl
W2WquE0mGH/wv9FMWg+4Y6tnstWP2mukuVRte9PSPYBl29cExDcxbLMC/suTAkAF
jLl/o8k8iOLd+xFEWKYpz8mfTU4LO/qwhRGj3SU2fbzJgPjSj3Ej8J/NZ/zxqzZb
QvcdCpQT7O7gT51yrPTzAkEAt4SopUbbd1sRNQZjrHSN611ymNdYHf4RKP9LquOI
gdmhejU/CBlp7CAQqui8H3VVJuvkO1BJhSTorxSW21vvwA==
-----END RSA PRIVATE KEY-----

-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDj6Uvptmo3u21Us5Z+QclmNSKr
QpM2ubMaj6kz/fE+FIiOldeyUzvS5wA9k3MiS+PXE1gsU5xDmVVI7hHYRWdAvZiM
TGS3dxXYAYnprTL4/wub6Y/96uxrcPoHKxtGIrQt2jqGhFGcitD1nWmdEtPjee/3
fhduAuEiCtjOnqit1QIDAQAB
-----END PUBLIC KEY-----

###### 将public key转换成XML
https://superdry.apphb.com/tools/online-rsa-key-converter

###### 将时间转换为Epoch timestamp
https://www.epochconverter.com/
