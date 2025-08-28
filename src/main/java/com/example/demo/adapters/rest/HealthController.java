package com.example.demo.adapters.rest;

import com.example.demo.application.health.HealthQueryService;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(path = "/health", produces = MediaType.TEXT_PLAIN_VALUE)
public class HealthController {
    private final HealthQueryService healthQueryService;

    public HealthController(HealthQueryService healthQueryService) {
        this.healthQueryService = healthQueryService;
    }

    @GetMapping
    public String health() {
        return healthQueryService.health();
    }
}
