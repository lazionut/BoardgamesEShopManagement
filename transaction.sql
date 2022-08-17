BEGIN TRY
	BEGIN TRANSACTION
		
		UPDATE Boardgame
		SET Name = 'Azul: Limited Edition'
		WHERE Id = 2
		
		UPDATE Review
		SET Title = 'Moderated title'
		WHERE Id = 4
		
		UPDATE Address
		SET Phone = '0712345671'
		WHERE Id = 1 OR Id = 10

        DELETE FROM Wishlist
		WHERE Id = 4
		
		DELETE FROM OrderItem
		WHERE OrderId = 6 AND BoardgameId = 8 
		
	COMMIT TRANSACTION
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH