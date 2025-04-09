PRAGMA table_info('MarketplacePremios');

SELECT sql 
FROM sqlite_master 
WHERE type = 'table' AND name = 'MarketplacePremios';

SELECT *
FROM MarketplacePremios mp
LEFT JOIN Organizaciones o 
ON mp.OrganizacionId = o.OrganizacionId
WHERE o.Nombre = 'ULatina'


