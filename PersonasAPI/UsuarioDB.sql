-- Created by GitHub Copilot in VSCode MSSQL - review carefully before executing
-- Crear usuario contenido en la base de datos con contraseña
CREATE USER felipefbcApi WITH PASSWORD = '71295150Fel@'  -- Reemplaza con una contraseña segura

-- Asignar permisos necesarios mediante roles de base de datos
ALTER ROLE db_datareader ADD MEMBER felipefbcApi  -- Permiso de lectura
ALTER ROLE db_datawriter ADD MEMBER felipefbcApi  -- Permiso de escritura
ALTER ROLE db_ddladmin ADD MEMBER felipefbcApi    -- Permiso para modificar esquema

-- Otorgar permisos de ejecución
GRANT EXECUTE TO felipefbcApi