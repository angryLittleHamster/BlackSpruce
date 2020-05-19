using BlackSpruce.Exceptions;
using BlackSpruce.Shared.Model.Exceptions;
using BlackSpruce.Shared.Patterns.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace BlackSpruce.Shared.Tests.ChainOfResponsibilityTests
{
    public class ChainOfResponsibilityPatternShould
    {
        [Theory]
        [InlineData("Vader", "Darth", "900723456", 1977, 03, 31, "CA")]
        public void HandleCitizenValidationSample(string lastName, string firstName, string sin, int? year, int? month, int? day, string region)
        {
            //Arrange
            var newCitizen = new Citizen()
            {
                LastName = lastName,
                FirstName = firstName,
                SIN = sin,
                PrimaryRegion = new CitizenshipRegion() { TwoLetterIsoRegionName = region }
            };

            if (year.HasValue && month.HasValue && day.HasValue)
            {
                newCitizen.DateOfBirth = new DateTime(year.Value, month.Value, day.Value);
            }
            var processor = new CitizenProcessor();
            //Action
            var result = processor.Register(newCitizen);

            //Assert

            Assert.True(result);
        }

    }

    #region CitizenHandler classes
    internal class CitizenProcessor
    {
        public bool Register(Citizen citizen)
        {
            try
            {
                var handler = new SocialInsuranceNumberValidatorHandler();
                handler.SetNext(new AgeValidationHandler())
                       .SetNext(new NameValidationHandler())
                       .SetNext(new CitizenshipRegionValidationHandler());

                handler?.Handle(citizen);
            }
            catch (UserValidationException ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

    }
    internal class Citizen
    {
        private string _socialInsuranceNumber;
        private CitizenshipRegion _primaryRegion;

        public CitizenshipRegion PrimaryRegion
        {
            get { return _primaryRegion; }
            set { _primaryRegion = value; }
        }
        public string SocialInsuranceNumber
        {
            get { return _socialInsuranceNumber; }
            set { _socialInsuranceNumber = value; }
        }
        public string SIN 
        {
            get { return _socialInsuranceNumber; }
            set { _socialInsuranceNumber = value; }
        
        }
        public DateTime? DateOfBirth { get; set; }
        public int? Age
        {
            get
            {
                if (DateOfBirth != null)
                {
                    // Save today's date.
                    var today = DateTime.Today;
                    // Calculate the age.
                    var age = today.Year - (DateOfBirth?.Year ?? 0);
                    // Go back to the year the person was born in case of a leap year
                    if (DateOfBirth?.Date > today.AddYears(-age)) age--;
                    return age;
                }
                else
                {
                    return null;
                }


            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Name => LastName == null ? $"{LastName}, {FirstName}" : $"{FirstName}";
    }
    internal class CitizenshipRegion
    {
        [StringLength(2)]
        public string TwoLetterIsoRegionName { get; set; }
    }
    internal class SocialInsuranceNumberValidator
    {
        public bool Validate(string socialInsuranceNumber, CitizenshipRegion region )
        {
            if (socialInsuranceNumber is null || region?.TwoLetterIsoRegionName is null)
            {
                return false;
            }
            return true;
        }
    }
    internal class SocialInsuranceNumberValidatorHandler : Handler<Citizen>
    {
        private readonly SocialInsuranceNumberValidator socialInsuranceNumbervalidator = new SocialInsuranceNumberValidator();
        public override void Handle(Citizen citizen)
        {
            if (citizen is null)
            {
                throw new HandlerException($"HandlerException: {nameof(SocialInsuranceNumberValidatorHandler)} has a Handle to a null citizen");
            }
            if (!socialInsuranceNumbervalidator.Validate(citizen.SocialInsuranceNumber, citizen.PrimaryRegion))
            {
                throw new UserValidationException("Social Insurance Number could not be validated");

            }
            base.Handle(citizen);
        }
    }
    internal class AgeValidationHandler : Handler<Citizen>
    {
        public override void Handle(Citizen citizen)
        {
            if (citizen is null)
            {
                throw new HandlerException($"HandlerException: {nameof(AgeValidationHandler)} has a Handle to a null citizen");
            }
            if (citizen.Age< 18)
            {
                throw new UserValidationException("You have to be  years or Older");
            }
            base.Handle(citizen);
        }
    }
    internal class NameValidationHandler : Handler<Citizen>
    {
        public override void Handle(Citizen citizen)
        {
            if (citizen is null)
            {
                throw new HandlerException($"HandlerException: {nameof(NameValidationHandler)} has a Handle to a null citizen");
            }
            if (citizen.Name?.Length <= 1)
            {
                throw new UserValidationException("Name is unlikely shorter than 1 character");
            }
            base.Handle(citizen);
        }

    }
    internal class CitizenshipRegionValidationHandler : Handler<Citizen>
    {
        public override void Handle(Citizen citizen)
        {
            if (citizen is null)
            {
                throw new HandlerException($"HandlerException: {nameof(CitizenshipRegionValidationHandler)} has a Handle to a null citizen");
            }
            if (citizen.PrimaryRegion == null)
            {
                throw new UserValidationException("We currently do not support citizens that do not have a Region");
            }
            if (citizen.PrimaryRegion.TwoLetterIsoRegionName == "US")
            {
                throw new UserValidationException("We currently do not support USA citizens");
            }
            base.Handle(citizen);
        }
    }

    #endregion CitizenHandler classes

    


}
