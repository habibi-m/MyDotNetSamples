﻿using FeatureFlagsDemo.Interfaces;
using FeatureFlagsDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlagsDemo.Controllers
{
    [Authorize]
    [FeatureGate(nameof(FeatureFlags.MobileReview))]
    public class MobileController : Controller
    {
        private readonly IMobileDataService _mobileDataService;

        public MobileController(IMobileDataService mobileDataService)
        {
            _mobileDataService = mobileDataService;
        }

        public IActionResult Index()
        {
            List<Mobile> mobiles = _mobileDataService.GetAllMobileReviews();
            return View(mobiles);
        }

        [FeatureGate(nameof(FeatureFlags.MobileDetailedReview))]
        public IActionResult ReviewDetails(string? id)
        {
            Mobile? mobile = _mobileDataService.GetAllMobileReviews().Find(p => p.Id.Equals(id));
            return View(mobile);
        }
    }
}
