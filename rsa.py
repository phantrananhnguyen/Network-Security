import tkinter as tk
from tkinter import filedialog, messagebox, simpledialog, ttk
from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_OAEP
from Crypto.Signature import pkcs1_15
from Crypto.Hash import SHA256
from Crypto.Protocol.KDF import PBKDF2
from Crypto.Cipher import AES
import base64
import os
import json
import uuid
class RSAApp:
    def __init__(self, root):
        self.root = root
        self.root.title("RSA Encryption/Decryption and Digital Signatures")

        # Quản lý khóa
        self.key_frame = tk.LabelFrame(root, text="Key Management", padx=10, pady=10)
        self.key_frame.pack(fill=tk.X, padx=10, pady=5)
        # Khung hiển thị cặp khóa đã tạo
        self.key_list_frame = tk.LabelFrame(self.key_frame, text="Saved Keys", padx=10, pady=10)
        self.key_list_frame.grid(row=0, column=3, padx=30, pady=5, sticky="nsew", rowspan= 3)  # Sử dụng grid để đặt vị trí

        # Listbox cho khóa công khai
        self.public_key_listbox = tk.Listbox(self.key_list_frame, height=10, width=40)
        self.public_key_listbox.pack(side=tk.LEFT, fill=tk.Y, padx=5)

        # Scrollbar cho khóa công khai
        self.public_key_scrollbar = tk.Scrollbar(self.key_list_frame, orient="vertical", command=self.public_key_listbox.yview)
        self.public_key_scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        self.public_key_listbox.config(yscrollcommand=self.public_key_scrollbar.set)

        # Listbox cho khóa riêng
        self.private_key_listbox = tk.Listbox(self.key_list_frame, height=10, width=40)
        self.private_key_listbox.pack(side=tk.LEFT, fill=tk.Y, padx=5)

        # Scrollbar cho khóa riêng
        self.private_key_scrollbar = tk.Scrollbar(self.key_list_frame, orient="vertical", command=self.private_key_listbox.yview)
        self.private_key_scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        self.private_key_listbox.config(yscrollcommand=self.private_key_scrollbar.set)
        # Gắn sự kiện chọn khóa
        self.public_key_listbox.bind("<<ListboxSelect>>", self.on_public_key_selected)
        self.private_key_listbox.bind("<<ListboxSelect>>", self.on_private_key_selected)


        # Thêm Combobox cho người dùng chọn độ dài khóa
        tk.Label(self.key_frame, text="Select Key Length:").grid(row=0, column=0, padx=5, pady=5)
        self.key_length_combobox = ttk.Combobox(self.key_frame, values=["1024", "2048", "3072", "4096"])
        self.key_length_combobox.grid(row=0, column=1, padx=5, pady=5)
        self.key_length_combobox.set("2048")  # Đặt giá trị mặc định là 2048

        tk.Button(self.key_frame, text="Generate Keys", command=self.generate_keys, width=15).grid(row=1, column=0, padx=5, pady=5)
        tk.Button(self.key_frame, text="Open File...", command=self.open_file, width=15).grid(row=1, column=1, padx=5, pady=5)
        tk.Button(self.key_frame, text="Save Output", command=self.save_output, width=15).grid(row=2, column=0, padx=5, pady=5)
        tk.Button(self.key_frame, text="New Workspace", command=self.new_workspace, width=15).grid(row=2, column=1, padx=5, pady=5)

        # Chọn chế độ (Mã hóa/Giải mã/Chữ ký số)
        self.mode_frame = tk.LabelFrame(root, text="Choose Mode", padx=10, pady=10)
        self.mode_frame.pack(fill=tk.X, padx=10, pady=5)

        self.mode = tk.StringVar(value="encrypt")
        tk.Radiobutton(self.mode_frame, text="Encrypt", variable=self.mode, value="encrypt", command=self.switch_mode).pack(side=tk.LEFT, padx=10)
        tk.Radiobutton(self.mode_frame, text="Decrypt", variable=self.mode, value="decrypt", command=self.switch_mode).pack(side=tk.LEFT, padx=10)
        tk.Radiobutton(self.mode_frame, text="Sign", variable=self.mode, value="sign", command=self.switch_mode).pack(side=tk.LEFT, padx=10)
        tk.Radiobutton(self.mode_frame, text="Verify", variable=self.mode, value="verify", command=self.switch_mode).pack(side=tk.LEFT, padx=10)

        # Khu vực mã hóa
        self.encrypt_frame = tk.LabelFrame(root, text="Encryption", padx=10, pady=10)
        self.encrypt_frame.pack(fill=tk.X, padx=10, pady=5)

        tk.Label(self.encrypt_frame, text="Message to Encrypt").pack()
        self.encrypt_input = tk.Text(self.encrypt_frame, height=5)
        self.encrypt_input.pack()
        tk.Button(self.encrypt_frame, text="Encrypt", command=self.encrypt_message).pack(pady=5)
        self.encrypted_output = tk.Text(self.encrypt_frame, height=5)
        self.encrypted_output.pack()

        # Khu vực giải mã
        self.decrypt_frame = tk.LabelFrame(root, text="Decryption", padx=10, pady=10)
        self.decrypt_frame.pack(fill=tk.X, padx=10, pady=5)

        tk.Label(self.decrypt_frame, text="Encrypted Message").pack()
        self.decrypt_input = tk.Text(self.decrypt_frame, height=5)
        self.decrypt_input.pack()
        tk.Button(self.decrypt_frame, text="Decrypt", command=self.decrypt_message).pack(pady=5)
        self.decrypted_output = tk.Text(self.decrypt_frame, height=5)
        self.decrypted_output.pack()

        # Khu vực chữ ký số
        self.sign_frame = tk.LabelFrame(root, text="Digital Signatures", padx=10, pady=10)
        self.sign_frame.pack(fill=tk.X, padx=10, pady=5)

        tk.Label(self.sign_frame, text="Message to Sign").pack()
        self.sign_input = tk.Text(self.sign_frame, height=5)
        self.sign_input.pack()
        tk.Button(self.sign_frame, text="Sign", command=self.sign_message).pack(pady=5)
        self.signature_output = tk.Text(self.sign_frame, height=5)
        self.signature_output.pack()

        # Khu vực xác minh chữ ký
        self.verify_frame = tk.LabelFrame(root, text="Verify Signature", padx=10, pady=10)
        self.verify_frame.pack(fill=tk.X, padx=10, pady=5)

        tk.Label(self.verify_frame, text="Message").pack()
        self.verify_message_input = tk.Text(self.verify_frame, height=5)
        self.verify_message_input.pack()
        tk.Label(self.verify_frame, text="Signature").pack()
        self.verify_signature_input = tk.Text(self.verify_frame, height=5)
        self.verify_signature_input.pack()
        tk.Button(self.verify_frame, text="Verify", command=self.verify_signature).pack(pady=5)
        self.key_storage_path = os.path.expanduser("~/my_rsa_keys")
        
        # Các biến chứa khóa
        self.private_key = None
        self.public_key = None
        self.processing_event = False
        self.load_key_list()

        self.switch_mode()
    def new_workspace(self):
        """Làm mới input và output để người dùng có thể bắt đầu lại."""
        # Xóa nội dung trong các trường nhập và xuất
        self.encrypt_input.delete("1.0", tk.END)
        self.decrypt_input.delete("1.0", tk.END)
        self.encrypted_output.delete("1.0", tk.END)
        self.decrypted_output.delete("1.0", tk.END)
        self.sign_input.delete("1.0",tk.END)
        self.signature_output.delete("1.0",tk.END)
        self.verify_signature_input.delete("1.0", tk.END)
        self.verify_message_input.delete("1.0", tk.END)
        # Đặt lại các giá trị cần thiết nếu có
        self.public_key = None
        self.private_key = None

    def display_keys_in_listbox(self):
        """Hiển thị các cặp khóa vào các Listbox tương ứng"""
        key_storage_path = os.path.expanduser("~/my_rsa_keys")
        files = os.listdir(key_storage_path)

        # Lọc danh sách tệp
        public_keys = [f for f in files if f.startswith("public_key_") and f.endswith(".pem")]
        private_keys = [f for f in files if f.startswith("private_key_") and f.endswith(".pem")]

        # Hiển thị khóa công khai (chỉ ID duy nhất)
        self.public_key_listbox.delete(0, tk.END)
        for key in public_keys:
            unique_id = key.replace("public_key_", "").replace(".pem", "")  # Chỉ giữ ID
            self.public_key_listbox.insert(tk.END, unique_id)

        # Hiển thị khóa riêng (chỉ ID duy nhất)
        self.private_key_listbox.delete(0, tk.END)
        for key in private_keys:
            unique_id = key.replace("private_key_", "").replace(".pem", "")  # Chỉ giữ ID
            self.private_key_listbox.insert(tk.END, unique_id)


    def on_public_key_selected(self, event):
        """Xử lý khi chọn public key từ danh sách."""
        try:
            if self.processing_event:
                return
            self.processing_event = True

            # Lấy tên file đầy đủ từ danh sách
            selected_indices = self.public_key_listbox.curselection()
            if not selected_indices:
                raise ValueError("No public key selected")

            # Lấy tên tệp công khai đã chọn
            selected_item = self.public_key_listbox.get(selected_indices)
            public_key_filename = selected_item.strip()

            # Trích xuất key_length và unique_id từ tên tệp
            parts = public_key_filename.split('_')
            key_length = int(parts[2])
            unique_id = parts[3].split('.')[0]

            # Tìm khóa riêng tương ứng
            private_key_filename = f"private_key_{key_length}_{unique_id}.pem"
            private_key_path = os.path.join(self.key_storage_path, private_key_filename)

            # Kiểm tra nếu khóa riêng tương ứng không tồn tại
            if not os.path.exists(private_key_path):
                messagebox.showwarning("Warning", f"No corresponding private key found for {public_key_filename}")

            # Đọc và hiển thị khóa công khai
            public_key_path = os.path.join(self.key_storage_path, public_key_filename)
            if not os.path.exists(public_key_path):
                raise FileNotFoundError(f"Public key file not found: {public_key_path}")

            with open(public_key_path, "rb") as f:
                self.public_key = f.read()

            messagebox.showinfo("Public Key", f"Public key loaded successfully:\n{public_key_filename}")

        except Exception as e:
            messagebox.showerror("Error", f"Unable to load public key: {str(e)}")
        finally:
            self.processing_event = False



    def on_private_key_selected(self, event):
        """Xử lý khi chọn private key từ danh sách."""
        try:
            if self.processing_event:
                return
            self.processing_event = True

            # Lấy tên file đầy đủ từ danh sách
            selected_indices = self.private_key_listbox.curselection()
            if not selected_indices:
                raise ValueError("No private key selected")

            # Lấy tên tệp riêng đã chọn
            selected_item = self.private_key_listbox.get(selected_indices)
            private_key_filename = selected_item.strip()

            # Trích xuất key_length và unique_id từ tên tệp
            parts = private_key_filename.split('_')
            key_length = int(parts[2])
            unique_id = parts[3].split('.')[0]

            # Tìm khóa công khai tương ứng
            public_key_filename = f"public_key_{key_length}_{unique_id}.pem"
            public_key_path = os.path.join(self.key_storage_path, public_key_filename)

            # Kiểm tra nếu khóa công khai tương ứng không tồn tại
            if not os.path.exists(public_key_path):
                messagebox.showwarning("Warning", f"No corresponding public key found for {private_key_filename}")

            # Đọc và hiển thị khóa riêng
            private_key_path = os.path.join(self.key_storage_path, private_key_filename)
            if not os.path.exists(private_key_path):
                raise FileNotFoundError(f"Private key file not found: {private_key_path}")

            with open(private_key_path, "rb") as f:
                self.private_key = f.read()

            messagebox.showinfo("Private Key", f"Private key loaded successfully:\n{private_key_filename}")

        except Exception as e:
            messagebox.showerror("Error", f"Unable to load private key: {str(e)}")
        finally:
            self.processing_event = False

    def switch_mode(self):
        """Chuyển đổi giao diện theo chế độ đã chọn."""
        self.encrypt_frame.pack_forget()
        self.decrypt_frame.pack_forget()
        self.sign_frame.pack_forget()
        self.verify_frame.pack_forget()

        if self.mode.get() == "encrypt":
            self.encrypt_frame.pack(fill=tk.X, padx=10, pady=5)
        elif self.mode.get() == "decrypt":
            self.decrypt_frame.pack(fill=tk.X, padx=10, pady=5)
        elif self.mode.get() == "sign":
            self.sign_frame.pack(fill=tk.X, padx=10, pady=5)
        elif self.mode.get() == "verify":
            self.verify_frame.pack(fill=tk.X, padx=10, pady=5)


    def generate_keys(self):
        """Tự động tạo và lưu cặp khóa RSA vào thư mục được chỉ định, cập nhật Listbox."""
        try:
            # Lấy độ dài khóa từ combobox
            key_length = int(self.key_length_combobox.get())
            if key_length <= 0:
                messagebox.showwarning("Error", "Key length must be greater than zero.")
                return

            # Tạo khóa RSA
            key = RSA.generate(key_length)
            self.private_key = key.export_key()
            self.public_key = key.publickey().export_key()

            # Tạo thư mục lưu trữ cặp khóa
            key_storage_path = os.path.expanduser("~/my_rsa_keys")
            os.makedirs(key_storage_path, exist_ok=True)

            # Đặt tên tệp khóa (dùng thời gian hoặc UUID để đảm bảo duy nhất)
            unique_id = str(uuid.uuid4())  # Tạo UUID duy nhất
            private_filename = f"private_key_{key_length}_{unique_id}.pem"
            public_filename = f"public_key_{key_length}_{unique_id}.pem"

            # Lưu khóa riêng
            private_path = os.path.join(key_storage_path, private_filename)
            with open(private_path, "wb") as f:
                f.write(self.private_key)

            # Lưu khóa công khai
            public_path = os.path.join(key_storage_path, public_filename)
            with open(public_path, "wb") as f:
                f.write(self.public_key)

            # Cập nhật Listbox với cặp khóa mới
            self.public_key_listbox.insert(tk.END, public_filename) 
            self.private_key_listbox.insert(tk.END, private_filename)
            # Lưu danh sách khóa vào tệp (Đảm bảo phương thức này hoạt động đúng)
            self.save_key_list(private_filename, public_filename)

            messagebox.showinfo("Success", f"Keys generated and saved in {key_storage_path}!")
        except Exception as e:
            messagebox.showerror("Error", f"Failed to generate keys: {str(e)}")

    def save_key_list(self, private_filename, public_filename):
            """Lưu danh sách tên tệp khóa vào tệp JSON."""
            key_storage_path = os.path.expanduser("~/my_rsa_keys")
            key_list_file = os.path.join(key_storage_path, "keys_list.json")

            # Kiểm tra và tạo tệp nếu chưa tồn tại
            if not os.path.exists(key_list_file):
                with open(key_list_file, "w") as f:
                    json.dump([], f, indent=4)

            try:
                with open(key_list_file, "r") as f:
                    key_list = json.load(f)
            except Exception as e:
                key_list = []

            # Thêm cặp khóa mới vào danh sách
            key_list.append({"private": private_filename, "public": public_filename})

            # Ghi lại danh sách vào tệp
            with open(key_list_file, "w") as f:
                json.dump(key_list, f, indent=4)

        

    def load_key_list(self):
        """Tải danh sách cặp khóa từ tệp JSON và hiển thị trong Listbox."""
        key_storage_path = os.path.expanduser("~/my_rsa_keys")
        key_list_file = os.path.join(key_storage_path, "keys_list.json")

        if os.path.exists(key_list_file):
            try:
                # Mở và đọc tệp JSON chứa danh sách các cặp khóa
                with open(key_list_file, "r") as f:
                    key_list = json.load(f)

                # Cập nhật Listbox khóa công khai
                self.public_key_listbox.delete(0, tk.END)  # Xóa danh sách cũ
                self.private_key_listbox.delete(0, tk.END)  # Xóa danh sách cũ
                for key_pair in key_list:
                    # Thêm khóa công khai vào public_key_listbox
                    self.public_key_listbox.insert(tk.END, key_pair['public'])

                    # Thêm khóa riêng vào private_key_listbox
                    self.private_key_listbox.insert(tk.END, key_pair['private'])

            except json.JSONDecodeError as e:
                messagebox.showerror("Error", f"JSON format error in key list file: {str(e)}")
            except Exception as e:
                messagebox.showerror("Error", f"Failed to load key list: {str(e)}")
        else:
            messagebox.showinfo("Info", "Key list file does not exist.")


    def save_output(self):
        """Save the output (encrypted message, decrypted message, or signature) to a file."""
        try:
            output_text = ""
            
            # Check which mode is selected and get the appropriate output
            if self.mode.get() == "encrypt":
                output_text = self.encrypted_output.get("1.0", tk.END)
            elif self.mode.get() == "decrypt":
                output_text = self.decrypted_output.get("1.0", tk.END)
            elif self.mode.get() == "sign":
                output_text = self.signature_output.get("1.0", tk.END)

            # Ask user where to save the file
            file_path = filedialog.asksaveasfilename(defaultextension=".txt", filetypes=[("Text files", "*.txt")])
            if file_path:
                with open(file_path, "w") as file:
                    file.write(output_text.strip())  # Remove any extra newlines or spaces
                
                messagebox.showinfo("Success", f"Output saved to {file_path}")
            else:
                messagebox.showwarning("No file selected", "No file was selected to save the output.")
        except Exception as e:
            messagebox.showerror("Error", f"Failed to save output: {str(e)}")


    
    def open_file(self):
        """Mở tệp và hiển thị nội dung vào trường nhập liệu dựa trên chế độ hiện tại"""
        file_path = filedialog.askopenfilename(title="Select file")
        if not file_path:
            return  # Nếu không chọn tệp

        try:
            with open(file_path, "r") as file:
                file_content = file.read()

            # Tùy thuộc vào chế độ hiện tại, hiển thị nội dung vào trường nhập liệu tương ứng
            if self.mode.get() == "encrypt":
                self.encrypt_input.delete("1.0", tk.END)
                self.encrypt_input.insert(tk.END, file_content)
            elif self.mode.get() == "decrypt":
                self.decrypt_input.delete("1.0", tk.END)
                self.decrypt_input.insert(tk.END, file_content)
            elif self.mode.get() == "sign":
                # Hiển thị vào phần ký (nếu có trường nhập liệu cho ký)
                pass
            elif self.mode.get() == "verify":
                # Hiển thị vào phần xác minh (nếu có trường nhập liệu cho xác minh)
                pass

        except Exception as e:
            messagebox.showerror("Error", f"Unable to open file: {str(e)}")


    def encrypt_message(self):
        """Mã hóa thông điệp"""
        if not self.public_key:
            messagebox.showwarning("Error", "Load a public key first!")
            return

        message = self.encrypt_input.get("1.0", tk.END).strip()
        if not message:
            messagebox.showwarning("Error", "Message is empty!")
            return

        try:
            key = RSA.import_key(self.public_key)
            cipher = PKCS1_OAEP.new(key)
            encrypted_message = cipher.encrypt(message.encode())
            self.encrypted_output.delete("1.0", tk.END)
            self.encrypted_output.insert(tk.END, base64.b64encode(encrypted_message).decode())
            messagebox.showinfo("Success", "Message encrypted!")
        except Exception as e:
            messagebox.showerror("Error", str(e))

    def decrypt_message(self):
        """Giải mã thông điệp"""
        if not self.private_key:
            messagebox.showwarning("Error", "Load a private key first!")
            return

        encrypted_message = self.decrypt_input.get("1.0", tk.END).strip()
        if not encrypted_message:
            messagebox.showwarning("Error", "Encrypted message is empty!")
            return

        try:
            key = RSA.import_key(self.private_key)
            cipher = PKCS1_OAEP.new(key)
            decrypted_message = cipher.decrypt(base64.b64decode(encrypted_message.encode()))
            self.decrypted_output.delete("1.0", tk.END)
            self.decrypted_output.insert(tk.END, decrypted_message.decode())
            messagebox.showinfo("Success", "Message decrypted!")
        except Exception as e:
            messagebox.showerror("Error", str(e))
    
    def sign_message(self):
        """Chữ ký số"""
        if not self.private_key:
            messagebox.showwarning("Error", "Load a private key first!")
            return

        message = self.sign_input.get("1.0", tk.END).strip()
        if not message:
            messagebox.showwarning("Error", "Message is empty!")
            return

        try:
            key = RSA.import_key(self.private_key)
            hash_obj = SHA256.new(message.encode())
            signer = pkcs1_15.new(key)
            signature = signer.sign(hash_obj)
            self.signature_output.delete("1.0", tk.END)
            self.signature_output.insert(tk.END, base64.b64encode(signature).decode())
            messagebox.showinfo("Success", "Message signed!")
        except Exception as e:
            messagebox.showerror("Error", str(e))
    def verify_signature(self):
            """Verify the digital signature of a message."""
            if not self.public_key:
                messagebox.showwarning("Error", "Load a public key first!")
                return

            message = self.verify_message_input.get("1.0", tk.END).strip()
            signature = self.verify_signature_input.get("1.0", tk.END).strip()

            if not message or not signature:
                messagebox.showwarning("Error", "Message or signature is empty!")
                return

            try:
                key = RSA.import_key(self.public_key)
                hash_obj = SHA256.new(message.encode())
                verifier = pkcs1_15.new(key)
                verifier.verify(hash_obj, base64.b64decode(signature))
                messagebox.showinfo("Success", "Signature is valid!")
            except (ValueError, TypeError) as e:
                messagebox.showerror("Error", "Signature is invalid!")
            except Exception as e:
                messagebox.showerror("Error", str(e))


if __name__ == "__main__":
    root = tk.Tk()
    app = RSAApp(root)
    root.mainloop()
