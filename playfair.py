import tkinter as tk
from tkinter import ttk, messagebox


def create_matrix(key):
    include_numbers = any(char.isdigit() for char in key)
    key = "".join(dict.fromkeys(key.upper().replace("J", "I") if not include_numbers else key.upper()))
    alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ" if not include_numbers else "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    alphabet = "".join([c for c in alphabet if c not in key])

    final_key = key + alphabet
    size = 5 if not include_numbers else 6
    matrix = [list(final_key[i * size:(i + 1) * size]) for i in range(size)]
    return matrix, key, include_numbers


def locate_char(matrix, char):
    for i, row in enumerate(matrix):
        for j, col in enumerate(row):
            if col == char:
                return i, j
    return None


def process_text(text, include_numbers):
    text = text.upper().replace("J", "I") if not include_numbers else text.upper()
    text = ''.join(filter(str.isalnum, text))
    if len(text) % 2 != 0:
        text += 'X'
    return text


def playfair_cipher(text, key, mode):
    matrix, _, include_numbers = create_matrix(key)
    size = len(matrix)
    processed_text = process_text(text, include_numbers)
    result = []

    for i in range(0, len(processed_text), 2):
        a, b = processed_text[i], processed_text[i + 1]
        row_a, col_a = locate_char(matrix, a)
        row_b, col_b = locate_char(matrix, b)

        if row_a == row_b:
            if mode == "encrypt":
                result.append(matrix[row_a][(col_a + 1) % size])
                result.append(matrix[row_b][(col_b + 1) % size])
            else:
                result.append(matrix[row_a][(col_a - 1) % size])
                result.append(matrix[row_b][(col_b - 1) % size])
        elif col_a == col_b:
            if mode == "encrypt":
                result.append(matrix[(row_a + 1) % size][col_a])
                result.append(matrix[(row_b + 1) % size][col_b])
            else:
                result.append(matrix[(row_a - 1) % size][col_a])
                result.append(matrix[(row_b - 1) % size][col_b])
        else:
            result.append(matrix[row_a][col_b])
            result.append(matrix[row_b][col_a])

    return ''.join(result)


def update_matrix_display(matrix, key):
    for widget in matrix_frame.winfo_children():
        widget.destroy()

    size = len(matrix)
    cell_size = 40 

    for i in range(size):
        for j in range(size):
            char = matrix[i][j]
            label = ttk.Label(matrix_frame, text=char, font=("Arial", 12), borderwidth=1, relief="solid", width=3, anchor="center")
            if char in key:
                label.config(font=("Arial", 12, "bold"), foreground="blue")
            label.grid(row=i, column=j, padx=0, pady=0, sticky="NSEW")

    # Cập nhật kích thước cửa sổ cho vừa ma trận
    root.geometry(f"{cell_size * size + 150}x{cell_size * size + 150}")


def handle_action():
    mode = mode_var.get()
    key = key_entry.get()
    text = text_entry.get()

    if not key or not text:
        messagebox.showerror("Error", "Key and text must be filled.")
        return

    matrix, normalized_key, _ = create_matrix(key)
    update_matrix_display(matrix, normalized_key)

    if mode == "Encrypt":
        result = playfair_cipher(text, key, "encrypt")
    else:
        result = playfair_cipher(text, key, "decrypt")

    result_label.config(text=f"Result: {result}")


# Giao diện Tkinter
root = tk.Tk()
root.title("Playfair Cipher")

# Thiết lập các hàng và cột có thể thay đổi kích thước
root.grid_rowconfigure(0, weight=1)  
root.grid_columnconfigure(0, weight=1)  

# Frame chính
frame = ttk.Frame(root, padding=10)
frame.grid(row=0, column=0, sticky="NSEW")
frame.pack_propagate(False)

# Thành phần giao diện
ttk.Label(frame, text="Plaintext:").grid(row=0, column=0, sticky="W", pady=5)
text_entry = ttk.Entry(frame, width=40)
text_entry.grid(row=0, column=1, pady=5)

ttk.Label(frame, text="Key:").grid(row=1, column=0, sticky="W", pady=5)
key_entry = ttk.Entry(frame, width=40)
key_entry.grid(row=1, column=1, pady=5)

ttk.Label(frame, text="Mode:").grid(row=2, column=0, sticky="W", pady=5)
mode_var = tk.StringVar(value="Encrypt")
mode_combobox = ttk.Combobox(frame, textvariable=mode_var, values=["Encrypt", "Decrypt"], width=37)
mode_combobox.grid(row=2, column=1, pady=5)
mode_combobox.config(state="readonly")  # Thiết lập chế độ "readonly"

ttk.Button(frame, text="OK", command=handle_action).grid(row=3, column=1, pady=10, sticky="E")

result_label = ttk.Label(frame, text="Result: ", font=("Arial", 12, "bold"))
result_label.grid(row=4, column=0, columnspan=2, pady=10)

matrix_frame = ttk.Frame(root, padding=10)
matrix_frame.grid(row=1, column=0, columnspan=2, sticky="NSEW")
root.grid_rowconfigure(1, weight=1) 
root.grid_columnconfigure(0, weight=2) 

root.mainloop()
