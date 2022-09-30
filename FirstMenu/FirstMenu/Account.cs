using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMenu
{
    public class Account
    {
        // =============================================================================================================================
        //                                                    DECLARING VARIABLES
        // =============================================================================================================================
       
        //creating the variables for each input on Account object
        private String firstName;
        private String lastName;
        private String middleInitial;
        private String suffix;
        private String email;
        private String phoneNum;
        private String SSNSalt;
        private String SSNHash;
        private String dateOfBirth;
        private String street;
        private String state;
        private String county;
        private String accountUsername;
        private String accountPasswordSalt;
        private String accountPasswordHash;
        private String accountType;
        private String zipCode;
        private String accountPinCodeSalt;
        private String accountPinCodeHash;
        private String routingNum;
        private String question1;
        private String answer1Salt;
        private String answer1Hash;
        private String question2;
        private String answer2Salt;
        private String answer2Hash;
        private String question3;
        private String answer3Salt;
        private String answer3Hash;

        // =============================================================================================================================
        //                                                    ACCOUNT OBJECT
        // =============================================================================================================================

        public Account(
            String firstName, 
            String middleInitial, 
            String lastName, 
            String suffix, 
            String email, 
            String phoneNum, 
            String SSNSalt,
            String SSNHash,
            String dateOfBirth, 
            String street,
            String state, 
            String county, 
            String zipCode,
            String accountUsername, 
            String accountPasswordSalt,
            String accountPasswordHash,
            String accountType, 
            String accountPinCodeSalt,
            String accountPinCodeHash,
            String routingNum,
            String question1,
            String answer1Salt,
            String answer1Hash,
            String question2,
            String answer2Salt,
            String answer2Hash,
            String question3,
            String answer3Salt,
            String answer3Hash)
            // ^^ what is needed for the account object
        {
            // vv assigning the entered inputs to the local variables
            this.firstName = firstName;
            this.middleInitial = middleInitial;
            this.lastName = lastName;
            this.suffix = suffix;
            this.email = email;
            this.phoneNum = phoneNum;
            this.SSNSalt = SSNSalt;
            this.SSNHash = SSNHash;
            this.dateOfBirth = dateOfBirth;
            this.street = street;
            this.state = state;
            this.county = county;
            this.zipCode = zipCode;
            this.accountUsername = accountUsername;
            this.accountPasswordSalt = accountPasswordSalt;
            this.accountPasswordHash = accountPasswordHash;
            this.accountType = accountType;
            this.accountPinCodeSalt = accountPinCodeSalt;
            this.accountPinCodeHash = accountPinCodeHash;
            this.routingNum = routingNum;
            this.question1 = question1;
            this.answer1Salt = answer1Salt;
            this.answer1Hash = answer1Hash;
            this.question2 = question2;
            this.answer2Salt = answer2Salt;
            this.answer2Hash = answer2Hash;
            this.question3 = question3;
            this.answer3Salt = answer3Salt;
            this.answer3Hash = answer3Hash;
        }

        // =============================================================================================================================
        //                                                    FIRST NAME
        // =============================================================================================================================

        public String FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        // =============================================================================================================================
        //                                                    MIDDLE INITIAL
        // =============================================================================================================================

        public String MiddleInitial
        {
            get
            {
                if (middleInitial.Length <= 1)
                {
                    return middleInitial;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (value.Length <= 1)
                {
                    this.middleInitial = value;
                }
                else
                {
                    this.middleInitial = "";
                }
            }
        }

        // =============================================================================================================================
        //                                                    LAST NAME
        // =============================================================================================================================

        public String LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        // =============================================================================================================================
        //                                                    SUFFIX
        // =============================================================================================================================

        public String Suffix
        {
            get
            {
                return suffix;
            }
            set
            {
                this.suffix = value;
            }
        }

        // =============================================================================================================================
        //                                                    EMAIL
        // =============================================================================================================================

        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
            }
        }

        // =============================================================================================================================
        //                                                    PHONE NUMBER
        // =============================================================================================================================

        public String PhoneNumber
        {
            get
            {
                return phoneNum;
            }
            set
            {
                this.phoneNum = value;
            }
        }

        // =============================================================================================================================
        //                                                    SOCIAL SECURITY SALT
        // =============================================================================================================================

        public String SocialSecuritySalt
        {
            get
            {
                return SSNSalt;
            }
            set
            {
                this.SSNSalt = value;
            }
        }

        // =============================================================================================================================
        //                                                    SOCIAL SECURITY HASH
        // =============================================================================================================================

        public String SocialSecurityHash
        {
            get
            {
                return SSNHash;
            }
            set
            {
                this.SSNHash = value;
            }
        }

        // =============================================================================================================================
        //                                                    DATE OF BIRTH
        // =============================================================================================================================

        public String DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                this.dateOfBirth = value;
            }
        }

        // =============================================================================================================================
        //                                                    STREET
        // =============================================================================================================================

        public String Street
        {
            get
            {
                return street;
            }
            set
            {
                this.street = value;
            }
        }

        // =============================================================================================================================
        //                                                    STATE
        // =============================================================================================================================

        public String State
        {
            get
            {
                return state;
            }
            set
            {
                this.state = value;
            }
        }

        // =============================================================================================================================
        //                                                    COUNTY
        // =============================================================================================================================

        public String County
        {
            get
            {
                return county;
            }
            set
            {
                this.county = value;
            }
        }

        // =============================================================================================================================
        //                                                    ZIP CODE
        // =============================================================================================================================

        public String ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }

        // =============================================================================================================================
        //                                                    USERNAME
        // =============================================================================================================================

        public String Username
        {
            get
            {
                return accountUsername;
            }
            set
            {
                this.accountUsername = value;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD SALT
        // =============================================================================================================================

        public String PasswordSalt
        {
            get
            {
                return accountPasswordSalt;
            }
            set
            {
                this.accountPasswordSalt = value;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD HASH
        // =============================================================================================================================

        public String PasswordHash
        {
            get
            {
                return accountPasswordHash;
            }
            set
            {
                this.accountPasswordHash = value;
            }
        }

        // =============================================================================================================================
        //                                                    ACCOUNT TYPE
        // =============================================================================================================================

        public String AccountType
        {
            get
            {
                return accountType;
            }
            set
            {
                this.accountType = value;
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE SALT
        // =============================================================================================================================

        public String PinCodeSalt
        {
            get
            {
                return accountPinCodeSalt;
            }
            set
            {
                this.accountPinCodeSalt = value;
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE HASH
        // =============================================================================================================================

        public String PinCodeHash
        {
            get
            {
                return accountPinCodeHash;
            }
            set
            {
                this.accountPinCodeHash = value;
            }
        }

        // =============================================================================================================================
        //                                                    ROUTING NUMBER
        // =============================================================================================================================

        public String RoutingNum
        {
            get
            {
                return routingNum;
            }
            set
            {
                this.routingNum = value;
            }
        }

        // =============================================================================================================================
        //                                                    QUESTION 1
        // =============================================================================================================================

        public String Question1
        {
            get
            {
                return question1;
            }
            set
            {
                this.question1 = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 1 SALT
        // =============================================================================================================================

        public String Answer1Salt
        {
            get
            {
                return answer1Salt;
            }
            set
            {
                this.answer1Salt = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 1 HASH
        // =============================================================================================================================

        public String Answer1Hash
        {
            get
            {
                return answer1Hash;
            }
            set
            {
                this.answer1Hash = value;
            }
        }

        // =============================================================================================================================
        //                                                    QUESTION 2
        // =============================================================================================================================

        public String Question2
        {
            get
            {
                return question2;
            }
            set
            {
                this.question2 = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 2 SALT
        // =============================================================================================================================

        public String Answer2Salt
        {
            get
            {
                return answer2Salt;
            }
            set
            {
                this.answer2Salt = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 2 HASH
        // =============================================================================================================================

        public String Answer2Hash
        {
            get
            {
                return answer2Hash;
            }
            set
            {
                this.answer2Hash = value;
            }
        }

        // =============================================================================================================================
        //                                                    QUESTION 3
        // =============================================================================================================================

        public String Question3
        {
            get
            {
                return question3;
            }
            set
            {
                this.question3 = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 3 SALT
        // =============================================================================================================================

        public String Answer3Salt
        {
            get
            {
                return answer3Salt;
            }
            set
            {
                this.answer3Salt = value;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 3 HASH
        // =============================================================================================================================

        public String Answer3Hash
        {
            get
            {
                return answer3Hash;
            }
            set
            {
                this.answer3Hash = value;
            }
        }
    }
}