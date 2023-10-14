def write(content):
    print(content)

def warn(content):
    write(f"[WARNING] {content}")

def error(content):
    write(f"[ERROR] {content}")