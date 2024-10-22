import random
from sympy import isprime

# Task 1: Prime Number Operations
# 1.1 Generate random prime number with 8 bits, 16 bits, 64 bits
def generate_prime(bits):
    while True:
        candidate = random.getrandbits(bits)
        if isprime(candidate):
            return candidate

# 1.2 Determine the 10 largest prime numbers under the first 10 Mersenne prime numbers
def mersenne_primes(limit):
    primes = []
    p = 2
    while len(primes) < limit:
        mersenne = 2**p - 1
        if isprime(mersenne):
            primes.append(mersenne)
        p += 1
    return primes

# 1.3 Check if an arbitrary integer less than 2^89 - 1 is prime or not
def check_large_prime():
    max_value = 2**89 - 1  # Maximum value allowed
    number = int(input(f"Enter an integer less than {max_value}: "))  # User input
    if number >= max_value:
        print(f"Number must be smaller than {max_value}.")
        return False
    return isprime(number)

# Task 2: Greatest Common Divisor (GCD) using Euclidean Algorithm
def euclid_gcd(a, b):
    while b != 0:
        a, b = b, a % b
    return a

# Generate two large random integers
def generate_large_integer(bits):
    return random.getrandbits(bits)

# Task 3: Modular Exponentiation using Exponentiation by Squaring
def mod_exp(base, exp, mod):
    result = 1
    base = base % mod  # Ensure base is within modulus
    while exp > 0:
        if exp % 2 == 1:  # If exp is odd
            result = (result * base) % mod
        base = (base * base) % mod
        exp //= 2
    return result

# Main function to run the tasks
if __name__ == "__main__":
    # Task 1: Prime number generation and Mersenne primes
    print("Random 8-bit prime:", generate_prime(8))
    print("Random 16-bit prime:", generate_prime(16))
    print("Random 64-bit prime:", generate_prime(64))
    mersenne_prime_list = mersenne_primes(10)
    print("First 10 Mersenne primes:", mersenne_prime_list)
    print("Is the entered number prime?:", check_large_prime())
    # Task 2: GCD of two large numbers
    a = generate_large_integer(256)
    b = generate_large_integer(256)
    print(f"First large integer (a): {a}")
    print(f"Second large integer (b): {b}")
    print(f"GCD of a and b is: {euclid_gcd(a, b)}")
    # Task 3: Modular exponentiation example
    base = generate_large_integer(32) % (2 ** 32)  
    exp = random.randint(41, 2**16)
    mod = generate_large_integer(32) % (2 ** 32) + 1 
    print(f"Random base: {base}, Random exponent: {exp}, Random modulus: {mod}")
    print(f"{base}^{exp} mod {mod} is: {mod_exp(base, exp, mod)}")
