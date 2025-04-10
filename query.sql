PRAGMA table_info('MarketplacePremios');

SELECT sql 
FROM sqlite_master 
WHERE type = 'table' AND name = 'MarketplacePremios';

SELECT *
FROM Organizaciones o
LEFT JOIN Colaboradores c
ON o.OrganizacionId = c.OrganizacionId
WHERE o.Nombre = 'ULatina'



