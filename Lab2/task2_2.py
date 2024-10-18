import random
from math import gcd
from sympy import isprime

# Function to find the modular inverse of e modulo φ(n)
def mod_inverse(e, phi):
    def egcd(a, b):
        if a == 0:
            return b, 0, 1
        else:
            g, x, y = egcd(b % a, a)
            return g, y - (b // a) * x, x

    g, x, y = egcd(e, phi)
    if g != 1:
        raise Exception('Modular inverse does not exist.')
    else:
        return x % phi

# Function to generate prime numbers (used if p and q are not provided)
def generate_prime(bitsize):
    while True:
        num = random.getrandbits(bitsize)
        if isprime(num):
            return num

# RSA keypair generation
def generate_keypair(p=None, q=None, e=None, bitsize=1024):
    if not p or not q:
        p = generate_prime(bitsize // 2)
        q = generate_prime(bitsize // 2)
    
    n = p * q
    phi = (p - 1) * (q - 1)
    
    if not e:
        e = random.randrange(1, phi)
        while gcd(e, phi) != 1:  # Ensure e is coprime with φ(n)
            e = random.randrange(1, phi)
    
    d = mod_inverse(e, phi)
    
    # Public key (e, n) and Private key (d, n)
    return ((e, n), (d, n))

# Encryption function
def encrypt(public_key, message):
    e, n = public_key
    # If message is a string, convert to numeric using ord()
    if isinstance(message, str):
        message = int.from_bytes(message.encode('utf-8'), 'big')
    return pow(message, e, n)

# Decryption function
def decrypt(private_key, ciphertext):
    d, n = private_key
    decrypted_message = pow(ciphertext, d, n)
    
    # Attempt to convert the numeric message back to a string
    try:
        return decrypted_message.to_bytes((decrypted_message.bit_length() + 7) // 8, 'big').decode('utf-8')
    except:
        return decrypted_message

# Function to ensure input is a prime number
def get_prime_input(prompt):
    while True:
        try:
            num = input(prompt)
            if not num:
                return None  # Return None if user leaves input blank
            num = int(num)
            if isprime(num):
                return num
            else:
                print(f"{num} không phải là số nguyên tố. Vui lòng nhập lại.")
        except ValueError:
            print("Giá trị không hợp lệ. Vui lòng nhập lại.")

# Main function to test the RSA implementation with user input
def rsa_test():
    # Input for p, q, e (optional)
    p = get_prime_input("Nhập số nguyên tố p (hoặc để trống để sinh ngẫu nhiên): ")
    q = get_prime_input("Nhập số nguyên tố q (hoặc để trống để sinh ngẫu nhiên): ")
    
    try:
        e = input("Nhập số nguyên e (hoặc để trống để sinh ngẫu nhiên): ")
        e = int(e) if e else None
    except ValueError:
        print("Giá trị e không hợp lệ. Sử dụng giá trị ngẫu nhiên.")
        e = None

    # Input for message
    message = input("Nhập thông điệp để mã hóa (chuỗi hoặc số): ")
    try:
        message = int(message)
    except ValueError:
        pass  # Message remains a string if conversion fails
    
    # Generate the keypair (public and private keys)
    public_key, private_key = generate_keypair(p if p else None, q if q else None, e if e else None)
    print(f"\nKhóa công khai (Public Key): {public_key}")
    print(f"Khóa riêng (Private Key): {private_key}")
    
    # Encrypt the message
    print(f"\nThông điệp gốc: {message}")
    ciphertext = encrypt(public_key, message)
    print(f"Thông điệp sau khi mã hóa (Ciphertext): {ciphertext}")
    
    # Decrypt the message
    decrypted_message = decrypt(private_key, ciphertext)
    print(f"Thông điệp sau khi giải mã: {decrypted_message}")

# Test RSA with user input
rsa_test()
