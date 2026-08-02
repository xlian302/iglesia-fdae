import json
import unicodedata
import os

# 🔧 quitar tildes
def quitar_tildes(texto):
    return ''.join(
        c for c in unicodedata.normalize('NFD', texto)
        if unicodedata.category(c) != 'Mn'
    )

# 🔧 limpiar claves
def limpiar_claves(obj):
    if isinstance(obj, dict):
        nuevo = {}

        for k, v in obj.items():
            nueva_clave = quitar_tildes(str(k)).lower()
            nuevo[nueva_clave] = limpiar_claves(v)

        return nuevo

    elif isinstance(obj, list):
        return [limpiar_claves(i) for i in obj]

    else:
        return obj

# 📂 carpeta del script
BASE = os.path.dirname(__file__)

# 📥 archivo json
ruta_json = os.path.join(BASE, "himnos.json")

# 📤 archivo js
ruta_js = os.path.join(BASE, "himnos.js")

# 📖 cargar json
with open(ruta_json, "r", encoding="utf-8") as f:
    data = json.load(f)

# 🧹 limpiar datos
data_limpia = limpiar_claves(data)

# 💾 guardar js
with open(ruta_js, "w", encoding="utf-8") as f:
    f.write("const HIMNARIO = ")
    json.dump(data_limpia, f, ensure_ascii=False, indent=2)
    f.write(";")

print("✅ himnario.js creado correctamente")