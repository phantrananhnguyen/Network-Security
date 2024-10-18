from sympy import mod_inverse

# Giá trị cho trước (hexadecimal)
p3 = int('F7E75FDC469067FFDC4E847C51F452DF', 16)
q3 = int('E85CED54AF57E53E092113E62F436F4F', 16)
e3 = int('0D88C3', 16)

# Bước 1: Tính n
n3 = p3 * q3

# Bước 2: Tính φ(n)
phi_n3 = (p3 - 1) * (q3 - 1)

# Bước 3: Tính d (khóa bí mật)
d3 = mod_inverse(e3, phi_n3)

# In kết quả
print(f"n (modulus) = {n3}")
print(f"φ(n) = {phi_n3}")
print(f"Khóa công khai (e, n) = ({e3}, {n3})")
print(f"Khóa bí mật (d, n) = ({d3}, {n3})")


