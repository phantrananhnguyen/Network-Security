import hashlib
import tkinter as tk
from tkinter import filedialog, messagebox


# Function to calculate hash values
def calculate_hash(algorithm, data):
    hash_func = getattr(hashlib, algorithm)
    return hash_func(data).hexdigest()


# Function to handle file hash calculation
def hash_file():
    file_path = filedialog.askopenfilename(title="Select a File")
    if not file_path:
        return

    try:
        # Read the file content
        with open(file_path, "rb") as file:
            data = file.read()
        result = {
            "MD5": calculate_hash("md5", data),
            "SHA-1": calculate_hash("sha1", data),
            "SHA-256": calculate_hash("sha256", data),
        }

        # Display results
        result_text.set(
            f"File: {file_path}\n"
            f"MD5: {result['MD5']}\n"
            f"SHA-1: {result['SHA-1']}\n"
            f"SHA-256: {result['SHA-256']}"
        )
    except Exception as e:
        messagebox.showerror("Error", f"Failed to process file: {e}")


# Function to handle text/hash input
def hash_input():
    input_data = input_entry.get().strip()
    data_type = data_type_var.get()
    if not input_data:
        messagebox.showwarning("Input Required", "Please enter some input data.")
        return

    try:
        if data_type == "Text String":
            data = input_data.encode("utf-8")
        elif data_type == "Hex String":
            data = bytes.fromhex(input_data)
        else:
            raise ValueError("Unsupported data type")

        result = {
            "MD5": calculate_hash("md5", data),
            "SHA-1": calculate_hash("sha1", data),
            "SHA-256": calculate_hash("sha256", data),
        }

        # Display results
        result_text.set(
            f"Input: {input_data}\n"
            f"MD5: {result['MD5']}\n"
            f"SHA-1: {result['SHA-1']}\n"
            f"SHA-256: {result['SHA-256']}"
        )
    except Exception as e:
        messagebox.showerror("Error", f"Failed to process input: {e}")


# Create GUI window
root = tk.Tk()
root.title("Hash Calculator")

# Input Data Section
input_frame = tk.Frame(root)
input_frame.pack(pady=10)

tk.Label(input_frame, text="Input Data:").grid(row=0, column=0, padx=5, sticky="w")
input_entry = tk.Entry(input_frame, width=40)
input_entry.grid(row=0, column=1, padx=5, pady=5)

tk.Label(input_frame, text="Data Type:").grid(row=1, column=0, padx=5, sticky="w")
data_type_var = tk.StringVar(value="Text String")
data_type_menu = tk.OptionMenu(input_frame, data_type_var, "Text String", "Hex String")
data_type_menu.grid(row=1, column=1, padx=5, pady=5, sticky="w")

# Action Buttons
button_frame = tk.Frame(root)
button_frame.pack(pady=10)

hash_input_btn = tk.Button(button_frame, text="Hash Input", command=hash_input)
hash_input_btn.grid(row=0, column=0, padx=5)

hash_file_btn = tk.Button(button_frame, text="Hash File", command=hash_file)
hash_file_btn.grid(row=0, column=1, padx=5)

# Results Section
result_text = tk.StringVar(value="Results will appear here...")
result_label = tk.Label(root, textvariable=result_text, justify="left", anchor="w", bg="lightgray", width=60, height=10)
result_label.pack(pady=10, padx=10, fill="x")

# Run the application
root.mainloop()
