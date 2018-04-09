-- CREATE TABLE users (
--     id VARCHAR(255) NOT NULL,
--     username VARCHAR(20) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     password VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     UNIQUE KEY email (email)
-- );

-- CREATE TABLE vaults (
    -- id VARCHAR(255) NOT NULL,
    -- name VARCHAR(20) NOT NULL,
    -- description VARCHAR(255) NOT NULL,
    -- userId VARCHAR(255) NOT NULL,
    -- INDEX userId (userId),
    -- FOREIGN KEY (userId)
    --     REFERENCES users(id)
    --     ON DELETE CASCADE,  
    -- PRIMARY KEY (id)
-- );

-- CREATE TABLE keeps (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(20) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     saves int NOT NULL,
--     userId VARCHAR(255) NOT NULL,
--     INDEX userId (userId),
--     FOREIGN KEY (userId)
--         REFERENCES users(id)
--         ON DELETE CASCADE,  
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE vaultkeeps (
--     id VARCHAR(255) NOT NULL,
--     vaultId VARCHAR(255) NOT NULL,
--     keepId VARCHAR(255) NOT NULL,
--     userId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),
--     INDEX (vaultId, keepId),
--     INDEX (userId),

--     FOREIGN KEY (userId)
--         REFERENCES users(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (vaultId)
--         REFERENCES vaults(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (keepId)
--         REFERENCES keeps(id)
--         ON DELETE CASCADE
-- )

-- ALTER TABLE keeps
-- ALTER saves SET DEFAULT 0


-- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT * FROM vaultkeeps vk
-- INNER JOIN keeps k ON k.id = vk.keepId 
-- WHERE (vaultId = 2)