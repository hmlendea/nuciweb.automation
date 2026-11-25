# Logging

The repository contains no logging framework, logger, sink, log-level configuration, correlation mechanism, audit log, sampling policy, retention policy, or redaction implementation.

Backend and consuming applications own logging for browser commands, URLs, page data, exceptions, credentials, and correlation identifiers. They should avoid recording scripts, credentials, or personal data unless necessary and protected.
