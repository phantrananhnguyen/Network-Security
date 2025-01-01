import os
import subprocess
import tkinter as tk
from tkinter import messagebox

RSA_FILE = "rsa.py"
PLAYFAIR_FILE = "playfair.py"
# Hàm xử lý cho thuật toán RSA
def open_rsa():
    if os.path.exists(RSA_FILE):
       subprocess.run(["python", RSA_FILE])  # Windows
    else:
        tk.messagebox.showerror("Lỗi", f"Không tìm thấy file {RSA_FILE}")

def open_playfair():
    if os.path.exists(PLAYFAIR_FILE):
       subprocess.run(["python", PLAYFAIR_FILE])  # Windows
    else:
        tk.messagebox.showerror("Lỗi", f"Không tìm thấy file {PLAYFAIR_FILE}")
# Tạo giao diện chính
def main():
    root = tk.Tk()
    root.title("FakeCryptool")

    # Tạo nhãn tiêu đề
    title_label = tk.Label(root, text="FAKE CRYPTOOL", font=("Cambria", 16))
    title_label.pack(pady=20)

    # Nút chọn RSA
    rsa_button = tk.Button(root, text="RSA", font=("Cambria", 14), bg="lightblue", command=open_rsa)
    rsa_button.pack(pady=10, padx=50, fill=tk.X)

    # Nút chọn Playfair
    playfair_button = tk.Button(root, text="Playfair", font=("Cambria", 14), bg="lightgreen", command=open_playfair)
    playfair_button.pack(pady=10, padx=50, fill=tk.X)

    # Chạy giao diện
    root.mainloop()

if __name__ == "__main__":
    main()
