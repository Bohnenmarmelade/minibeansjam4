using NUnit.Framework;

namespace Tests
{
    public class TypeableWordTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void shouldSetToBeTyped_whenConstructed()
        {
            // given
            var word = "foobarbaz";
            var typeableWord = new TypeableWord(word);

            // then
            Assert.AreEqual(word, typeableWord.toBeTyped);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldSetSuccessfullyTyped_whenConstructed()
        {
            // given
            var word = "foobarbaz";
            var typeableWord = new TypeableWord(word);

            // then
            Assert.AreEqual("", typeableWord.succesfullyTyped);
        }
        
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldReturnTrue_whenCharacterIsCorrect()
        {
            // given
            var word = "foobarbaz";
            var typedChar = 'f';
            var typeableWord = new TypeableWord(word);

            // when
            var result = typeableWord.type(typedChar);
            
            // then
            Assert.IsTrue(result);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldUpdateSuccessfullyAndToBeTyped_whenCharacterIsCorrect()
        {
            // given
            var word = "foobarbaz";
            var typedChar = 'f';
            var typeableWord = new TypeableWord(word);

            // when
            typeableWord.type(typedChar);
            
            // then
            Assert.AreEqual(typedChar.ToString(), typeableWord.succesfullyTyped);
            Assert.AreEqual("oobarbaz", typeableWord.toBeTyped);
        } 
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldUpdateSuccessfullyAndToBeTyped_whenCharacterIsCorrectAndUpperCase()
        {
            // given
            var word = "foobarbaz";
            var typedChar = 'F';
            var typeableWord = new TypeableWord(word);

            // when
            typeableWord.type(typedChar);
            
            // then
            Assert.AreEqual(typedChar.ToString().ToLower(), typeableWord.succesfullyTyped);
            Assert.AreEqual("oobarbaz", typeableWord.toBeTyped);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldReturnFalse_whenCharacterIsIncorrect()
        {
            // given
            var word = "foobarbaz";
            var typedChar = 'b';
            var typeableWord = new TypeableWord(word);

            // when
            var result = typeableWord.type(typedChar);
            
            // then
            Assert.IsFalse(result);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void shouldNotUpdateSuccessfullyAndToBeTyped_whenCharacterIsIncorrect()
        {
            // given
            var word = "foobarbaz";
            var typedChar = 'b';
            var typeableWord = new TypeableWord(word);

            // when
            typeableWord.type(typedChar);
            
            // then
            Assert.AreEqual("", typeableWord.succesfullyTyped);
            Assert.AreEqual(word, typeableWord.toBeTyped);
        }
    }
    
}